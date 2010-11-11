﻿using System;
using System.Configuration;
using System.ServiceProcess;
using Config=System.Configuration;

using Health.Direct.Config.Client;
using Health.Direct.Common.Diagnostics;

using ClientSettingsSection=Health.Direct.Config.Client.ClientSettingsSection;

namespace Health.Direct.DnsResponder.WinSrv
{
    public partial class DnsResponderWinSrv : ServiceBase
    {
        protected DnsResponderTCP m_dnsResponderTCP = null;
        protected DnsResponderUDP m_dnsResponderUDP = null;
        protected DnsServer m_dnsServer = null;
        protected DnsRecordStorageService m_store = null;
        private readonly ILogger m_logger;

        public DnsResponderWinSrv()
        {

            InitializeComponent();
            try
            {
                m_logger = Log.For(this);
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex);
                throw;
            }
        }


        private ILogger Logger
        {
            get { return m_logger; }
        }


        private static void WriteToEventLog(Exception ex)
        {
            const string source = "Health.Direct.DnsResponder.WinSrv";

            EventLogHelper.WriteError(source, ex.Message);
            EventLogHelper.WriteError(source, ex.GetBaseException().ToString());
        }



        private static void WriteToEventLogWarn(string message)
        {
            const string source = "Health.Direct.DnsResponder.WinSrv";

            EventLogHelper.WriteWarning(source, message);
        }

        private static void WriteToEventLogInfo(string message)
        {
            const string source = "Health.Direct.DnsResponder.WinSrv";

            EventLogHelper.WriteInformation(source, message);
        }

        /// <summary>
        /// method to initialize fields utilized by the service
        /// </summary>
        protected void InitializeService()

        {
            WriteToEventLogInfo("Service is being initialized");

            //----------------------------------------------------------------------------------------------------
            //---load the settings from the related sections in app.config
            ClientSettings dnsRecordServiceSettings = ((ClientSettingsSection)ConfigurationManager.GetSection("ServiceSettingsGroup/DnsRecordManagerServiceSettings")).AsClientSettings();
            ClientSettings certServiceSettings = ((ClientSettingsSection)ConfigurationManager.GetSection("ServiceSettingsGroup/CertificateManagerServiceSettings")).AsClientSettings();
            DnsServerSettings dnsServerSettings = ((DnsServerSettingsSection)ConfigurationManager.GetSection("ServiceSettingsGroup/DnsServerSettings")).AsDnsServerSettings();

            m_store = new DnsRecordStorageService(dnsRecordServiceSettings, certServiceSettings);

            //----------------------------------------------------------------------------------------------------
            //---create the DNS Server instance
            m_dnsServer = new DnsServer(m_store
                , dnsServerSettings);

            //----------------------------------------------------------------------------------------------------
            //---init the tcp and udp responders
            m_dnsResponderTCP = new DnsResponderTCP(m_dnsServer);
            m_dnsResponderUDP = new DnsResponderUDP(m_dnsServer);
            m_dnsServer.UDPResponder.Server.Error += UDPServer_Error;
            m_dnsServer.TCPResponder.Server.Error += TCPServer_Error;
            WriteToEventLogInfo("Service has been fully initialized");
        }

        public void StartService(string[] args)
        {
            InitializeService();
            m_dnsResponderTCP.Start();
            m_dnsResponderUDP.Start();
        }

        public void StopService()
        {

            m_dnsResponderTCP.Stop();
            m_dnsResponderUDP.Stop();
            
        }

        protected override void OnStart(string[] args)
        {
            this.StartService(args);
        }

        protected override void OnStop()
        {
            this.StopService();
        }

        void TCPServer_Error(Exception ex)
        {
            WriteToEventLog(ex);
        }

        void UDPServer_Error(Exception ex)
        {
            WriteToEventLog(ex);
        }
    }
}