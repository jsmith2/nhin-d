
package ihe.iti.xds_b._2007;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;
import javax.jws.soap.SOAPBinding;
import javax.xml.bind.annotation.XmlSeeAlso;
import oasis.names.tc.ebxml_regrep.xsd.rs._3.RegistryResponseType;


/**
 * This class was generated by the JAX-WS RI.
 * JAX-WS RI 2.1.3-b02-
 * Generated source version: 2.1
 * 
 */
@WebService(name = "DocumentRepository_PortType", targetNamespace = "urn:ihe:iti:xds-b:2007")
@SOAPBinding(parameterStyle = SOAPBinding.ParameterStyle.BARE)
@XmlSeeAlso({
    oasis.names.tc.ebxml_regrep.xsd.query._3.ObjectFactory.class,
    oasis.names.tc.ebxml_regrep.xsd.lcm._3.ObjectFactory.class,
    oasis.names.tc.ebxml_regrep.xsd.rs._3.ObjectFactory.class,
    oasis.names.tc.ebxml_regrep.xsd.rim._3.ObjectFactory.class,
    ihe.iti.xds_b._2007.ObjectFactory.class
})
public interface DocumentRepositoryPortType {


    /**
     * 
     * @param body
     * @return
     *     returns oasis.names.tc.ebxml_regrep.xsd.rs._3.RegistryResponseType
     */
    @WebMethod(operationName = "DocumentRepository_ProvideAndRegisterDocumentSet-b", action = "urn:ihe:iti:2007:ProvideAndRegisterDocumentSet-b")
    @WebResult(name = "RegistryResponse", targetNamespace = "urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0", partName = "body")
    public RegistryResponseType documentRepositoryProvideAndRegisterDocumentSetB(
        @WebParam(name = "ProvideAndRegisterDocumentSetRequest", targetNamespace = "urn:ihe:iti:xds-b:2007", partName = "body")
        ProvideAndRegisterDocumentSetRequestType body);

    /**
     * 
     * @param body
     * @return
     *     returns ihe.iti.xds_b._2007.RetrieveDocumentSetResponseType
     */
    @WebMethod(operationName = "DocumentRepository_RetrieveDocumentSet", action = "urn:ihe:iti:2007:RetrieveDocumentSet")
    @WebResult(name = "RetrieveDocumentSetResponse", targetNamespace = "urn:ihe:iti:xds-b:2007", partName = "body")
    public RetrieveDocumentSetResponseType documentRepositoryRetrieveDocumentSet(
        @WebParam(name = "RetrieveDocumentSetRequest", targetNamespace = "urn:ihe:iti:xds-b:2007", partName = "body")
        RetrieveDocumentSetRequestType body);

}
