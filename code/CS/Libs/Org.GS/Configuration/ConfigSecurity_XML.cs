/*


      <ProgramFunctionControl>
        <ProgramFunctionSet>
          <ProgramFunction FunctionNumber="1" FunctionName="StandardUse" />
          <ProgramFunction FunctionNumber="2" FunctionName="ViewLocalAppConfig" />
          <ProgramFunction FunctionNumber="3" FunctionName="EditLocalAppConfig" />
          <ProgramFunction FunctionNumber="4" FunctionName="ViewCentralAppConfig" />
          <ProgramFunction FunctionNumber="5" FunctionName="EditCentralAppConfig" />
          <ProgramFunction FunctionNumber="6" FunctionName="ViewRoleCapabilities" />
          <ProgramFunction FunctionNumber="7" FunctionName="ViewAppLog" />
          <ProgramFunction FunctionNumber="8" FunctionName="ClearAppLog" />
          <ProgramFunction FunctionNumber="9" FunctionName="ViewLocalFormConfigs" />
          <ProgramFunction FunctionNumber="10" FunctionName="EditLocalFormConfigs" />
          <ProgramFunction FunctionNumber="11" FunctionName="ViewCentralFormConfigs" />
          <ProgramFunction FunctionNumber="12" FunctionName="EditCentralFormConfigs" />
          <ProgramFunction FunctionNumber="13" FunctionName="UseDiagnosticsMode" />
          <ProgramFunction FunctionNumber="14" FunctionName="UseDeveloperFunctions" />
          <ProgramFunction FunctionNumber="15" FunctionName="UseEncryptedFileUtility" />
          <ProgramFunction FunctionNumber="16" FunctionName="RegisterScanner" />
          <ProgramFunction FunctionNumber="17" FunctionName="CleanupScannerRegistry" />
          <ProgramFunction FunctionNumber="18" FunctionName="CalibrateScannerBrightness" />
          <ProgramFunction FunctionNumber="19" FunctionName="EditScannerConfigurations" />
          <ProgramFunction FunctionNumber="20" FunctionName="GrantLicense" />
          <ProgramFunction FunctionNumber="999" FunctionName="Unlimited" />
        </ProgramFunctionSet>
        <ProgramRoleSet>
          <ProgramRole RoleNumber="1" OrgRoleName="PLStandardUser" ClientRoleName="PLStandardUser" FunctionsAllowed="1" InheritedRoles="" CLSecurity="False" Hide="False" />
          <ProgramRole RoleNumber="2" OrgRoleName="PLLocalAppAdmin" ClientRoleName="PLLocalAppAdmin" FunctionsAllowed="2,3,7,8,13,18,19" InheritedRoles="1" CLSecurity="False" Hide="False" />
          <ProgramRole RoleNumber="3" OrgRoleName="PLCentralAppAdmin" ClientRoleName="PLCentralAppAdmin" FunctionsAllowed="4,5" InheritedRoles="1,2" CLSecurity="False" Hide="False" />
          <ProgramRole RoleNumber="4" OrgRoleName="PLLocalFormCfgAdmin" ClientRoleName="PLLocalFormCfgAdmin" FunctionsAllowed="9,10" InheritedRoles="1" CLSecurity="False" Hide="False" />
          <ProgramRole RoleNumber="5" OrgRoleName="PLCentralFormCfgAdmin" ClientRoleName="PLCentralFormCfgAdmin" FunctionsAllowed="11,12" InheritedRoles="1,4" CLSecurity="False" Hide="False" />
          <ProgramRole RoleNumber="6" OrgRoleName="PLLocalSuperAdmin" ClientRoleName="PLLocalSuperAdmin" FunctionsAllowed="1" InheritedRoles="2,4" CLSecurity="False" Hide="False" />
          <ProgramRole RoleNumber="7" OrgRoleName="PLCentralSuperAdmin" ClientRoleName="PLCentralSuperAdmin" FunctionsAllowed="1" InheritedRoles="3,5" CLSecurity="False" Hide="False" />
          <ProgramRole RoleNumber="8" OrgRoleName="PLClientSuperAdmin" ClientRoleName="PLClientSuperAdmin" FunctionsAllowed="1" InheritedRoles="3,5" CLSecurity="False" Hide="False" />
          <ProgramRole RoleNumber="20" OrgRoleName="PLDeveloper" ClientRoleName="PLDeveloper" FunctionsAllowed="1" InheritedRoles="" CLSecurity="False" Hide="False" />
          <ProgramRole RoleNumber="21" OrgRoleName="PLSuperAdmin" ClientRoleName="PLSuperAdmin" FunctionsAllowed="1" InheritedRoles="" CLSecurity="False" Hide="False" />
          <ProgramRole RoleNumber="50" OrgRoleName="Admin" ClientRoleName="NotUsed" FunctionsAllowed="1" InheritedRoles="" CLSecurity="False" Hide="False" />
        </ProgramRoleSet>
      </ProgramFunctionControl>
    </ProgramConfig>
  </ProgramConfigSet>
  <NotificationConfig PatientLinkSupportEmail="" PatientLinkSupportPhone="">
    <SmtpProfileSet />
    <NotifyGroupSet />
    <NotifyEventSet />
  </NotificationConfig>
  <ProviderSet MaxActiveProviders="0" />
  <ConfigSecurity Source="" PasswordMinLth="6" PasswordMaxLth="20" PasswordReqMixCase="True" PasswordReqNbr="True" PasswordReqChgOnFirstUse="False" PasswordReqChgFreq="90">
    <UserSet>
      <User UserType="SecurityClass" UserID="001" UserName="PatientLink" FirstName="" LastName="" CompanyName="PatientLink" Password="plenable" PLDelete="True" Super="True">
        <GroupAssignments>
          <GroupAssignment GroupID="001" />
        </GroupAssignments>
      </User>
      <User UserType="SecurityClass" UserID="002" UserName="Client" FirstName="" LastName="" CompanyName="Client" Password="client" PLDelete="False" Super="False">
        <GroupAssignments>
          <GroupAssignment GroupID="001" />
        </GroupAssignments>
      </User>
    </UserSet>
    <GroupSet>
      <Group GroupID="001" GroupName="Admin" />
    </GroupSet>
  </ConfigSecurity>









*/