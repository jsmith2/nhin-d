package org.nhindirect.gateway.smtp.config.cert.impl;

import java.io.ByteArrayInputStream;
import java.io.File;
import java.security.Key;
import java.security.KeyStore;
import java.security.PrivateKey;
import java.security.Security;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

import org.apache.jcs.JCS;
import org.apache.jcs.access.exception.CacheException;
import org.apache.jcs.engine.behavior.ICompositeCacheAttributes;
import org.apache.jcs.engine.behavior.IElementAttributes;
import org.nhind.config.ConfigurationServiceProxy;
import org.nhindirect.stagent.CryptoExtensions;
import org.nhindirect.stagent.NHINDException;
import org.nhindirect.stagent.cert.CacheableCertStore;
import org.nhindirect.stagent.cert.CertStoreCachePolicy;
import org.nhindirect.stagent.cert.CertificateStore;
import org.nhindirect.stagent.cert.X509CertificateEx;
import org.nhindirect.stagent.cert.impl.KeyStoreCertificateStore;


/**
 * Certificate store backed by the configuration service.
 * @author Greg Meyer
 *
 */
public class ConfigServiceCertificateStore extends CertificateStore implements CacheableCertStore
{
	private static final String CACHE_NAME = "CONFIG_SERVICE_CERT_CACHE";
	
	private CertificateStore localStoreDelegate;
	private JCS cache;
	private CertStoreCachePolicy cachePolicy;
	private ConfigurationServiceProxy proxy;
	
	static
	{
		Security.addProvider(new org.bouncycastle.jce.provider.BouncyCastleProvider());
	}
	
	/**
	 * Constructs a service using the configuration service proxy and a default key store implementation for
	 * local lookups.
	 * @param proxy The configuration service proxy;
	 */
	public ConfigServiceCertificateStore(ConfigurationServiceProxy proxy)
	{
		setConfigurationServiceProxy(proxy);		
		localStoreDelegate = createDefaultLocalStore();
		loadBootStrap();
	}	


	
	/**
	 * Constructs a service using the configuration service proxy and a key store implementation for
	 * local lookups.
	 * @param proxy The configuration service proxy;
	 * @param bootstrapStore The certificate store used for local lookups.  This store is also the boot strap store.
	 * @param policy The certificate cache policy
	 */
	public ConfigServiceCertificateStore(ConfigurationServiceProxy proxy, 
			CertificateStore bootstrapStore, CertStoreCachePolicy policy)
	{
		if (bootstrapStore == null)
			throw new IllegalArgumentException();
		
		setConfigurationServiceProxy(proxy);
		
		this.cachePolicy = policy;			
		this.localStoreDelegate = bootstrapStore;			
		loadBootStrap();
	}	
	
	public void setConfigurationServiceProxy(ConfigurationServiceProxy proxy)
	{
		this.proxy = proxy;
	}	
	
	private synchronized JCS getCache()
	{
		if (cache == null)
			createCache();
		
		return cache;
	}
	
	private void createCache()
	{
		try
		{
			// create instance
			cache = JCS.getInstance(CACHE_NAME);		
			applyCachePolicy(cachePolicy == null ? getDefaultPolicy() : cachePolicy);
			cache.clear();
					
		}
		catch (CacheException e)
		{
			// TODO: log error
		}
	}
	
	private void applyCachePolicy(CertStoreCachePolicy policy)
	{
		if (getCache() != null)
		{
			try
			{
				ICompositeCacheAttributes attributes = cache.getCacheAttributes();
				attributes.setMaxObjects(policy.getMaxItems());
				attributes.setUseLateral(false);
				attributes.setUseRemote(false);
				cache.setCacheAttributes(attributes);
				
				IElementAttributes eattributes = cache.getDefaultElementAttributes();
				eattributes.setMaxLifeSeconds(policy.getSubjectTTL());
				eattributes.setIsEternal(false);
				eattributes.setIsLateral(false);
				eattributes.setIsRemote(false);		
				
				cache.setDefaultElementAttributes(eattributes);
			}
			catch (CacheException e)
			{
				// TODO: Handle exception
			}
		}
	}
	
	private CertStoreCachePolicy getDefaultPolicy()
	{
		return new DefaultConfigStoreCachePolicy();
	}
	
	/*
	 * Create the default local key store service.
	 */
	private CertificateStore createDefaultLocalStore()
	{
		KeyStoreCertificateStore retVal = new KeyStoreCertificateStore(new File("ConfigServiceKeyStore"), "nH!NdK3yStor3", "31visl!v3s");
		
		return retVal;
	}
	
	private static class DefaultConfigStoreCachePolicy implements CertStoreCachePolicy
	{

		public int getMaxItems() 
		{
			return 1000; 
		}

		public int getSubjectTTL() 
		{
			return 3600 * 24; // 1 day
		}
		
	}	
	
	/**
	 * {@inheritDoc}
	 */
    public boolean contains(X509Certificate cert)
    {
    	return localStoreDelegate == null ? false : localStoreDelegate.contains(cert);
    }	
    
	/**
	 * {@inheritDoc}
	 */
    public void add(X509Certificate cert)
    {
    	if (localStoreDelegate != null)
    		localStoreDelegate.add(cert);
    }    
	
	/**
	 * {@inheritDoc}
	 */
    public void remove(X509Certificate cert)
    {
    	if (localStoreDelegate != null)
    		localStoreDelegate.remove(cert);
    }    

	/**
	 * {@inheritDoc}
	 */  
    @SuppressWarnings("unchecked")
    @Override
    public Collection<X509Certificate> getCertificates(String subjectName)
    {
      	String realSubjectName;
    	int index;
		if ((index = subjectName.indexOf("EMAILADDRESS=")) > -1)
			realSubjectName = subjectName.substring(index + "EMAILADDRESS=".length());
		else
			realSubjectName = subjectName;
    	
    	Collection<X509Certificate> retVal;
    	
    	JCS cache = getCache();
    	
    	if (cache != null)
    	{
    		retVal = (Collection<X509Certificate>)cache.get(realSubjectName);
    		if (retVal == null || retVal.size() == 0)
    			retVal = this.lookupFromConfigStore(realSubjectName);
    	}
    	else // cache miss
    	{
    		retVal = this.lookupFromConfigStore(realSubjectName);
    		if (retVal.size() == 0 && localStoreDelegate != null)
    			retVal = localStoreDelegate.getCertificates(realSubjectName); // last ditch effort is to go to the bootstrap cache
    	}
    	
    	return retVal;
    }  
    
    private Collection<X509Certificate> lookupFromConfigStore(String subjectName)
    {    	
    	String domain;
    	
    	org.nhind.config.Certificate[] certificates;
    	try
    	{
    		certificates = proxy.getCertificatesForOwner(subjectName, null);
    	}
    	catch (Exception e)
    	{
    		throw new NHINDException("WebService error getting certificates by subject: " + e.getMessage(), e);
    	}
    	
    	if (certificates == null || certificates.length == 0)
    	{
    		// try again with the domain name
    		int index;
    		if ((index = subjectName.indexOf("@")) > -1)
    			domain = subjectName.substring(index + 1);
    		else
    			domain = subjectName;
    		
        	try
        	{
        		certificates = proxy.getCertificatesForOwner(domain, null);
        	}
        	catch (Exception e)
        	{
        		throw new NHINDException("WebService error getting certificates by domain: " + e.getMessage(), e);
        	}
    	}
    	
    	if (certificates == null || certificates.length == 0)
    		return Collections.emptyList();
    	
    	Collection<X509Certificate> retVal = new ArrayList<X509Certificate>();
    	for (org.nhind.config.Certificate cert : certificates)
    	{
    		X509Certificate storeCert = certFromData(cert.getData());
    		retVal.add(storeCert);
    		
			
			if(localStoreDelegate != null)
			{
				if (localStoreDelegate.contains(storeCert)) 
					localStoreDelegate.update(storeCert);
				else
					localStoreDelegate.add(storeCert);
			}
    	}
    	
		// add to JCS and cache
		try
		{
			if (cache != null)
				cache.put(subjectName, retVal);
		}
		catch (CacheException e)
		{
			/*
			 * TODO: handle exception
			 */
		}    	
    	
    	return retVal;
    }
    
	/**
	 * {@inheritDoc}
	 */
    @Override
    public Collection<X509Certificate> getAllCertificates()
    {
    	// get everything from the configuration service.... no caching here
    	org.nhind.config.Certificate[] certificates;
    	try
    	{
    		certificates = proxy.listCertificates(0L, 0x8FFF, null);  // hard code to get everything
    	}
    	catch (Exception e)
    	{
    		throw new NHINDException("WebService error getting all certificates: " + e.getMessage(), e);
    	}
    	 
    	// purge everything
    	this.flush(true);
    	
    	if (certificates == null || certificates.length == 0)
    		return Collections.emptyList();
    	
    	// convert to X509Certificates and store
    	Collection<X509Certificate> retVal = new ArrayList<X509Certificate>();
    	for (org.nhind.config.Certificate cert : certificates)
    	{
    		X509Certificate storeCert = certFromData(cert.getData());
    		retVal.add(storeCert);
    		
    		// add to JCS and cache
				try
				{
					if (cache != null)
						cache.put(cert.getOwner(), retVal);
				}
				catch (CacheException e)
				{
					/*
					 * TODO: handle exception
					 */
				}
				
				if(localStoreDelegate != null)
				{
					if (localStoreDelegate.contains(storeCert)) 
						localStoreDelegate.update(storeCert);
					else
						localStoreDelegate.add(storeCert);
				}
    	}
    	
    	return retVal;
    }    
    
    private X509Certificate certFromData(byte[] data)
    {
    	X509Certificate retVal = null;
        try 
        {
            ByteArrayInputStream bais = new ByteArrayInputStream(data);
            
            // lets try this a as a PKCS12 data stream first
            try
            {
            	KeyStore localKeyStore = KeyStore.getInstance("PKCS12", CryptoExtensions.getJCEProviderName());
            	
            	localKeyStore.load(bais, "".toCharArray());
            	Enumeration<String> aliases = localKeyStore.aliases();


        		// we are really expecting only one alias 
        		if (aliases.hasMoreElements())        			
        		{
        			String alias = aliases.nextElement();
        			X509Certificate cert = (X509Certificate)localKeyStore.getCertificate(alias);
        			
    				// check if there is private key
    				Key key = localKeyStore.getKey(alias, "".toCharArray());
    				if (key != null && key instanceof PrivateKey) 
    				{
    					retVal = X509CertificateEx.fromX509Certificate(cert, (PrivateKey)key);
    				}
    				else
    					retVal = cert;
    					
        		}
            }
            catch (Exception e)
            {
            	// must not be a PKCS12 stream, go on to next step
            }
   
            if (retVal == null)            	
            {
            	//try X509 certificate factory next       
                bais.reset();
                bais = new ByteArrayInputStream(data);

                retVal = (X509Certificate) CertificateFactory.getInstance("X.509").generateCertificate(bais);            	
            }
            bais.close();
        } 
        catch (Exception e) 
        {
            throw new NHINDException("Data cannot be converted to a valid X.509 Certificate", e);
        }
        
        return retVal;
    }
    
	public void flush(boolean purgeBootStrap) 
	{
		if (cache != null)
		{
			try
			{
				cache.clear();
			}
			catch (CacheException e)
			{
				/**
				 * TODO: handle exception
				 */
			}
		
			if (purgeBootStrap && this.localStoreDelegate != null)
			{
				localStoreDelegate.remove(localStoreDelegate.getAllCertificates());
			}
		}
	}
	
	public void loadBootStrap() 
	{
		if (localStoreDelegate == null)
			throw new IllegalStateException("The boot strap store has not been set.");
		

		JCS cache = null;
		if ((cache = getCache()) != null)
		{
			Map<String, Collection<X509Certificate>> cacheBuilderMap = new HashMap<String, Collection<X509Certificate>>();
			for (X509Certificate cert : localStoreDelegate.getAllCertificates())
			{
				/*
				 * TODO: need to decide how the entries/subjects will be indexed and named
				 */
			}
			
			for (Entry<String, Collection<X509Certificate>> entry : cacheBuilderMap.entrySet())
			{
				try
				{
					cache.put(entry.getKey(), entry.getValue());
				}
				catch (CacheException e)
				{
					/*
					 * TODO: handle exception
					 */
				}
			}
		}
	}

	public void loadBootStrap(CertificateStore bootstrapStore) 
	{
		if (localStoreDelegate == null)
		{
			throw new IllegalArgumentException();
		}
		this.localStoreDelegate = bootstrapStore;
		loadBootStrap();
	}

	public void setBootStrap(CertificateStore bootstrapStore) 
	{
		if (localStoreDelegate == null)
		{
			throw new IllegalArgumentException();
		}
		this.localStoreDelegate = bootstrapStore;		
	}

	public void setCachePolicy(CertStoreCachePolicy policy) 
	{		
		this.cachePolicy = policy;
		applyCachePolicy(policy);
	}	
	
}
