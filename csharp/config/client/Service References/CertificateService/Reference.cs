﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4952
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Health.Direct.Config.Client.CertificateService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CertificateGetOptions", Namespace="urn:directproject:config/store/082010")]
    [System.SerializableAttribute()]
    public partial class CertificateGetOptions : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IncludeDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IncludePrivateKeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<Health.Direct.Config.Store.EntityStatus> StatusField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IncludeData {
            get {
                return this.IncludeDataField;
            }
            set {
                if ((this.IncludeDataField.Equals(value) != true)) {
                    this.IncludeDataField = value;
                    this.RaisePropertyChanged("IncludeData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IncludePrivateKey {
            get {
                return this.IncludePrivateKeyField;
            }
            set {
                if ((this.IncludePrivateKeyField.Equals(value) != true)) {
                    this.IncludePrivateKeyField = value;
                    this.RaisePropertyChanged("IncludePrivateKey");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<Health.Direct.Config.Store.EntityStatus> Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:directproject:config/store/082010", ConfigurationName="CertificateService.ICertificateStore")]
    public interface ICertificateStore {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/ICertificateStore/AddCertificate", ReplyAction="urn:directproject:config/store/082010/ICertificateStore/AddCertificateResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/ICertificateStore/AddCertificateConfigStore" +
            "FaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Certificate AddCertificate(Health.Direct.Config.Store.Certificate certificate);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/ICertificateStore/AddCertificates", ReplyAction="urn:directproject:config/store/082010/ICertificateStore/AddCertificatesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/ICertificateStore/AddCertificatesConfigStor" +
            "eFaultFault", Name="ConfigStoreFault")]
        void AddCertificates(Health.Direct.Config.Store.Certificate[] certificates);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/ICertificateStore/GetCertificate", ReplyAction="urn:directproject:config/store/082010/ICertificateStore/GetCertificateResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/ICertificateStore/GetCertificateConfigStore" +
            "FaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Certificate GetCertificate(string owner, string thumbprint, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/ICertificateStore/GetCertificates", ReplyAction="urn:directproject:config/store/082010/ICertificateStore/GetCertificatesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/ICertificateStore/GetCertificatesConfigStor" +
            "eFaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Certificate[] GetCertificates(long[] certificateIDs, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/ICertificateStore/GetCertificatesForOwner", ReplyAction="urn:directproject:config/store/082010/ICertificateStore/GetCertificatesForOwnerRe" +
            "sponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/ICertificateStore/GetCertificatesForOwnerCo" +
            "nfigStoreFaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Certificate[] GetCertificatesForOwner(string owner, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/ICertificateStore/SetCertificateStatus", ReplyAction="urn:directproject:config/store/082010/ICertificateStore/SetCertificateStatusRespo" +
            "nse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/ICertificateStore/SetCertificateStatusConfi" +
            "gStoreFaultFault", Name="ConfigStoreFault")]
        void SetCertificateStatus(long[] certificateIDs, Health.Direct.Config.Store.EntityStatus status);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/ICertificateStore/SetCertificateStatusForOw" +
            "ner", ReplyAction="urn:directproject:config/store/082010/ICertificateStore/SetCertificateStatusForOw" +
            "nerResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/ICertificateStore/SetCertificateStatusForOw" +
            "nerConfigStoreFaultFault", Name="ConfigStoreFault")]
        void SetCertificateStatusForOwner(string owner, Health.Direct.Config.Store.EntityStatus status);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/ICertificateStore/RemoveCertificates", ReplyAction="urn:directproject:config/store/082010/ICertificateStore/RemoveCertificatesRespons" +
            "e")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/ICertificateStore/RemoveCertificatesConfigS" +
            "toreFaultFault", Name="ConfigStoreFault")]
        void RemoveCertificates(long[] certificateIDs);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/ICertificateStore/RemoveCertificatesForOwne" +
            "r", ReplyAction="urn:directproject:config/store/082010/ICertificateStore/RemoveCertificatesForOwne" +
            "rResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/ICertificateStore/RemoveCertificatesForOwne" +
            "rConfigStoreFaultFault", Name="ConfigStoreFault")]
        void RemoveCertificatesForOwner(string owner);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/ICertificateStore/EnumerateCertificates", ReplyAction="urn:directproject:config/store/082010/ICertificateStore/EnumerateCertificatesResp" +
            "onse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/ICertificateStore/EnumerateCertificatesConf" +
            "igStoreFaultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Certificate[] EnumerateCertificates(long lastCertificateID, int maxResults, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ICertificateStoreChannel : Health.Direct.Config.Client.CertificateService.ICertificateStore, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class CertificateStoreClient : System.ServiceModel.ClientBase<Health.Direct.Config.Client.CertificateService.ICertificateStore>, Health.Direct.Config.Client.CertificateService.ICertificateStore {
        
        public CertificateStoreClient() {
        }
        
        public CertificateStoreClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CertificateStoreClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CertificateStoreClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CertificateStoreClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Health.Direct.Config.Store.Certificate AddCertificate(Health.Direct.Config.Store.Certificate certificate) {
            return base.Channel.AddCertificate(certificate);
        }
        
        public void AddCertificates(Health.Direct.Config.Store.Certificate[] certificates) {
            base.Channel.AddCertificates(certificates);
        }
        
        public Health.Direct.Config.Store.Certificate GetCertificate(string owner, string thumbprint, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options) {
            return base.Channel.GetCertificate(owner, thumbprint, options);
        }
        
        public Health.Direct.Config.Store.Certificate[] GetCertificates(long[] certificateIDs, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options) {
            return base.Channel.GetCertificates(certificateIDs, options);
        }
        
        public Health.Direct.Config.Store.Certificate[] GetCertificatesForOwner(string owner, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options) {
            return base.Channel.GetCertificatesForOwner(owner, options);
        }
        
        public void SetCertificateStatus(long[] certificateIDs, Health.Direct.Config.Store.EntityStatus status) {
            base.Channel.SetCertificateStatus(certificateIDs, status);
        }
        
        public void SetCertificateStatusForOwner(string owner, Health.Direct.Config.Store.EntityStatus status) {
            base.Channel.SetCertificateStatusForOwner(owner, status);
        }
        
        public void RemoveCertificates(long[] certificateIDs) {
            base.Channel.RemoveCertificates(certificateIDs);
        }
        
        public void RemoveCertificatesForOwner(string owner) {
            base.Channel.RemoveCertificatesForOwner(owner);
        }
        
        public Health.Direct.Config.Store.Certificate[] EnumerateCertificates(long lastCertificateID, int maxResults, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options) {
            return base.Channel.EnumerateCertificates(lastCertificateID, maxResults, options);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:directproject:config/store/082010", ConfigurationName="CertificateService.IAnchorStore")]
    public interface IAnchorStore {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/AddAnchor", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/AddAnchorResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/AddAnchorConfigStoreFaultFault" +
            "", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Anchor AddAnchor(Health.Direct.Config.Store.Anchor anchor);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/AddAnchors", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/AddAnchorsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/AddAnchorsConfigStoreFaultFaul" +
            "t", Name="ConfigStoreFault")]
        void AddAnchors(Health.Direct.Config.Store.Anchor[] anchors);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/GetAnchor", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/GetAnchorResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/GetAnchorConfigStoreFaultFault" +
            "", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Anchor GetAnchor(string owner, string thumbprint, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/GetAnchors", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/GetAnchorsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/GetAnchorsConfigStoreFaultFaul" +
            "t", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Anchor[] GetAnchors(long[] anchorIDs, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/GetAnchorsForOwner", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/GetAnchorsForOwnerResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/GetAnchorsForOwnerConfigStoreF" +
            "aultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Anchor[] GetAnchorsForOwner(string owner, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/GetIncomingAnchors", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/GetIncomingAnchorsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/GetIncomingAnchorsConfigStoreF" +
            "aultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Anchor[] GetIncomingAnchors(string owner, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/GetOutgoingAnchors", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/GetOutgoingAnchorsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/GetOutgoingAnchorsConfigStoreF" +
            "aultFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Anchor[] GetOutgoingAnchors(string owner, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/SetAnchorStatus", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/SetAnchorStatusResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/SetAnchorStatusConfigStoreFaul" +
            "tFault", Name="ConfigStoreFault")]
        void SetAnchorStatus(long[] anchorIDs, Health.Direct.Config.Store.EntityStatus status);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/SetAnchorStatusForOwner", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/SetAnchorStatusForOwnerRespons" +
            "e")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/SetAnchorStatusForOwnerConfigS" +
            "toreFaultFault", Name="ConfigStoreFault")]
        void SetAnchorStatusForOwner(string owner, Health.Direct.Config.Store.EntityStatus status);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/EnumerateAnchors", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/EnumerateAnchorsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/EnumerateAnchorsConfigStoreFau" +
            "ltFault", Name="ConfigStoreFault")]
        Health.Direct.Config.Store.Anchor[] EnumerateAnchors(long lastAnchorID, int maxResults, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/RemoveAnchors", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/RemoveAnchorsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/RemoveAnchorsConfigStoreFaultF" +
            "ault", Name="ConfigStoreFault")]
        void RemoveAnchors(long[] anchorIDs);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:directproject:config/store/082010/IAnchorStore/RemoveAnchorsForOwner", ReplyAction="urn:directproject:config/store/082010/IAnchorStore/RemoveAnchorsForOwnerResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Health.Direct.Config.Store.ConfigStoreFault), Action="urn:directproject:config/store/082010/IAnchorStore/RemoveAnchorsForOwnerConfigSto" +
            "reFaultFault", Name="ConfigStoreFault")]
        void RemoveAnchorsForOwner(string owner);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IAnchorStoreChannel : Health.Direct.Config.Client.CertificateService.IAnchorStore, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class AnchorStoreClient : System.ServiceModel.ClientBase<Health.Direct.Config.Client.CertificateService.IAnchorStore>, Health.Direct.Config.Client.CertificateService.IAnchorStore {
        
        public AnchorStoreClient() {
        }
        
        public AnchorStoreClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AnchorStoreClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AnchorStoreClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AnchorStoreClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Health.Direct.Config.Store.Anchor AddAnchor(Health.Direct.Config.Store.Anchor anchor) {
            return base.Channel.AddAnchor(anchor);
        }
        
        public void AddAnchors(Health.Direct.Config.Store.Anchor[] anchors) {
            base.Channel.AddAnchors(anchors);
        }
        
        public Health.Direct.Config.Store.Anchor GetAnchor(string owner, string thumbprint, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options) {
            return base.Channel.GetAnchor(owner, thumbprint, options);
        }
        
        public Health.Direct.Config.Store.Anchor[] GetAnchors(long[] anchorIDs, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options) {
            return base.Channel.GetAnchors(anchorIDs, options);
        }
        
        public Health.Direct.Config.Store.Anchor[] GetAnchorsForOwner(string owner, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options) {
            return base.Channel.GetAnchorsForOwner(owner, options);
        }
        
        public Health.Direct.Config.Store.Anchor[] GetIncomingAnchors(string owner, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options) {
            return base.Channel.GetIncomingAnchors(owner, options);
        }
        
        public Health.Direct.Config.Store.Anchor[] GetOutgoingAnchors(string owner, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options) {
            return base.Channel.GetOutgoingAnchors(owner, options);
        }
        
        public void SetAnchorStatus(long[] anchorIDs, Health.Direct.Config.Store.EntityStatus status) {
            base.Channel.SetAnchorStatus(anchorIDs, status);
        }
        
        public void SetAnchorStatusForOwner(string owner, Health.Direct.Config.Store.EntityStatus status) {
            base.Channel.SetAnchorStatusForOwner(owner, status);
        }
        
        public Health.Direct.Config.Store.Anchor[] EnumerateAnchors(long lastAnchorID, int maxResults, Health.Direct.Config.Client.CertificateService.CertificateGetOptions options) {
            return base.Channel.EnumerateAnchors(lastAnchorID, maxResults, options);
        }
        
        public void RemoveAnchors(long[] anchorIDs) {
            base.Channel.RemoveAnchors(anchorIDs);
        }
        
        public void RemoveAnchorsForOwner(string owner) {
            base.Channel.RemoveAnchorsForOwner(owner);
        }
    }
}