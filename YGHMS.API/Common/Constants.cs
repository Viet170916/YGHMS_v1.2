using System.Diagnostics.CodeAnalysis;

// ReSharper disable All
namespace YGHMS.API.Common;

[ExcludeFromCodeCoverage]
public static class Constants
{
  public const string CorsPolicy = "CorsPolicy";
  public const string SwaggerEndpointName = "Kpmg BRP API";
  public const string SwaggerEndpointUrl = "/swagger/v1/swagger.json";
  public const string ApiVersion = "v1";
  public const string ApiTitle = "BRP API";
  public const string ConnStr = "ConnStr";
  public const string JwtDescription = "JWT Authorization header using the Bearer scheme.";
  public const string Bearer = "Bearer";
  public const string Authorization = "Authorization";
  public const string Jwt = "JWT";
  public const string ResponseHeader = "Access-Control-Allow-Origin";
  public const string ApplicationError = "Application-Error";
  public const string AccessControlExposeHeaders = "access-control-expose-headers";
  public const string Star = "*";
  public const string AspnetcoreEnvironment = "ASPNETCORE_ENVIRONMENT";
  public const string Development = "Development";
  public const string Production = "Production";
  public const string Success = "Success";
  public const string Failed = "Failed";
  public const string System = "System";
  public const string Yes = "Yes";
  public const string No = "No";
  public const string Single = "Single";
  public const string Error = "Error";
  public const string UserAlreadyExists = "User already exists!";
  public const string UserCreatedSuccessfully = "User created successfully!";
  public const string ApplicationJson = "application/json";
  public const string Json = ".json";
  public const string Minus = " - ";
  public const string FormatDate = "dd/MM/yyyy";
  public const string FormatDateExport = "dd-MMM-yy";
  public const string ApplicationPdf = "application/pdf";
  public const string ApplicationExcel = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
  public const string GetFileSuccess = "Get File Success";
  public const string ConvertBase64 = "data:application/pdf;base64 ";
  public const string LineBreak = "<br/>";
  public const string Permissions = "Permissions";
  public const string NoResultMatched = "No Result Matched !";
  public const string SearchDefaulStringValue = "-1";
  public const string StatusInactive = "0";
  public const string AuditFuntion = "Audit";
  public const string NonAuditFuntion = "Non-Audit";
  public const string ToCurrency = "SGD";
  public const int OneHundred = 100;
  public const string Delimiter = ", ";
  public const string StatusActive = "1";
  public const string Results = "Results";
  public const string N = "N";
  public const string Y = "Y";
  public const string FormatDateEmail = "dd MMM yyyy";
  public const string FormatDateTimeEmail = "dd MMM yyyy hh:mm";
  public const string Colon = ":";
  public const string Slash = "/";
  public const string Comma = ",";
  public const string Space = " ";
  public const string UsedByAnotherUser = "Used by another user";
  public const string Separator = ";";
  public const string SystemUser = "System User";
  public const string BatchUser = "Batch User";

  public static class ErrorMessages
  {
    public const string INVALID_EXPIRATION_SETED = "Post can't be activated if expiredate is not in future";
    public const string UNDER_5_IMAGES = "Please add more images";
    public const string LACK_OF_IMAGES = "Please add more images";
    public const string NO_ROOM = "Add more rooms to tenant can create reservations";
    public const string FORBIDDAN = "You are trying to access to other resources that they are not belong to you";
    public const string NO_APARTMENT = "No any apartment, post can not be activated";
    public const string RangeDateIsNull = "Checkin or checkout date have not selected yet";
    public const string ModelStateMessage = "Model State Invalid";
    public const string SuccessMessage = "Successfully";
    public const string NoRecordsFoundMessage = "No records found";
    public const string ValidationFailedMessage = "Validations Failed.";
    public const string RequestInvalid = "Request Invalid.";
    public const string ModelIsInvalidMessage = "Model is invalid.";

    public const string GeneralExceptionMessage =
      "Some error occurred. Please contact your system administrator.";

    public const string InvalidUserNameMessage = "Invalid Username or Password.";
    public const string NoRight = "Make sure you have correct right";
    public const string UnauthorizedUserMessage = "Unauthorized User.";
    public const string RoleNotExistedMessage = "Role Not Found.";
    public const string UserNotFoundMessage = "User Not Found";
    public const string InternalServerErrorMessage = "Internal Server Error.";
    public const string GetDataFailedPleaseTryAgain = "Get data failed. Please try again.";
    public const string InvalidInputDataType = "Template is empty";
    public const string TemplateHasNoSignatureField = "Template contains no signature field.";
    public const string NoSignature = "User has no signature.";
    public const string NotFound = "Data not found.";
    public const string ImageSizeExceedLimit = "Image exceeds the maximum resolution allowed (1920 x 1080)";
    public const string FileNotSupported = "The file format is not supported.";
    public const string Unauthorized = "Unauthorized.";
    public const string FileNotFound = "File Not Found";
    public const string POST_NOT_FOUND = "Post is not found";
    public const string NoFileDownload = "No file download.";
    public const string LimitedLength = "Limited length";
    public const string InvalidRole = "Invalid Role";
    public const string EmptyRecipient = "Empty recipient";
    public const string Over2MB = "The uploaded file is exceeding maximum size (2M)";
    public const string IndexSelected = "The Index is already selected";
    public const string BadParam = "Param can not null or empty";
    public const string BAD_PARAM_USER_FULLNAME = "Full name can not null or empty";
    public const string BAD_PARAM_USER_PHONENUMBER = "Phone number can not null or empty";
    public const string AddFail = "Can not add data";
    public const string BudgetGroupNotExisted = "The budget group not existed";
    public const string UpdateFail = "Can not update data";
    public const string DeleteFail = "Can not delete data";
    public const string SubGroupExisted = "The Subgroup already exists";
    public const string TaggingEntityExisted = "The Tagging Entity already exists";
    public const string TaggingEntityExistedPrimary = "The Tagging Entity and engagement code already exist";
    public const string SubconCostIsNotExisted = "The Sub Contract Cost is not exists";
    public const string NonBillableExpenseNotExisted = "The Non Billable Expense is not exists";
    public const string NotHavePermission = "Not Have Permission To Access";
    public const string EmployeeExisted = "The Employee already exists";
    public const string TargetChargeableHoursIsNotExisted = "The target chargeable hours is not exists";
    public const string FyNotNull = "Fy can not null";
  }

  public static class UrlConst
  {
    public const string GetBookingById = "api/booking/id?id=";
    public const string GetAllByListId = "api/sub-group/get-all-by-list-sub-id";
    public const string GetBookingAndBookingRequest = "api/booking/booking-and-booking-request";
    public const string GetBookingRequestDailyFactor = "api/booking/booking-request-daily-factor";
    public const string GetBookingRequestForEng = "api/booking/booking-request-for-eng-hour";

    public const string GetBookingRequestDailyFactorOverview =
      "api/booking/booking-request-daily-factor-overview";

    public const string GetAdjustmentToStaffHoursofBookingRequest =
      "api/booking/adjustment-staff-hour-of-booking-request";

    public const string GetSubgroupBySearch =
      "api/sub-group/get-subgroup-information-by-search?SubgroupName={0}&Month={1}&Year={2}&TypeOfWork={3}" +
      "&ClientGroupId={4}&SubGroupId={5}&YearEnd={6}";

    public const string GetSubgroupInfo = "api/sub-group/get-subgroup-info-by-client-id?clientNo=";
    public const string GetNotWorkingDay = "api/booking/calculate-not-working-day";
    public const string GetProfitCenterByBu = "api/common/get-profit-center-by-code?buNo=";
    public const string AddSubgroupCrs = " api/sub-group/add-subgroup";
    public const string GetAllKeyDeadLineType = "api/key-deadline/get-all-key-deadline-type/";
    public const string AddKeyDeadline = " api/key-deadline/add-key-deadline";
    public const string UpdateKeyDeadline = "api/key-deadline/update-key-deadline";
    public const string DeleteKeyDeadline = "api/key-deadline/delete-key-deadline";
    public const string SearchSubGroupByName = "api/sub-group/search-subgroup-by-name";
    public const string GetListSubgroupByName = "api/sub-group/get-list-subgroup-information-by-search";
    public const string GetKeyDeadlineSubgroup = "api/key-deadline/get-key-deadline-subgroup";
    public const string GetSubgroupInforByIds = "api/sub-group/get-subgroup-info-by-ids";
    public const string GetNotWorkingDayUpdate = "api/booking/calculate-not-working-day-update";
    public const string GetNotWorkingDayByFY = "api/booking/calculate-not-working-day-by-kpmgfy";
    public const string GetBookingRequestDailyFactorByFY = "api/booking/booking-request-daily-factor-by-fY";
    public const string GetCrsAdmin = "api/crs-employee/get-crs-admin-so";
    public const string CheckDeleteSubGroup = "api/sub-group/check-delete-subgroup?subGroupId=";
    public const string GetBuCooByBuNo = "api/common/get-bucoo-by-bu-no";
    public const string GetListGrade = "api/common/get-list-grade";

    public const string GetKeyDeadlineSubgroupByBudget =
      "api/key-deadline/get-key-deadline-subgroup-by-budget";

    public const string UpdateSubgroupCrs = " api/sub-group/update-subgroup";
    public const string GetSubInfo = "api/sub-group/get-all-sub-info";
  }

  public static class SessionConstants
  {
    public const string CurrentUserRole = "CurrentUserRole";
  }

  public static class Strings
  {
    public static class JwtClaimIdentifiers
    {
      public const string Rol = "rol", Id = "id";
    }

    public static class RoleIdentifiers
    {
      public const string Normal = "Normal";
      public const string Administrator = "Administrator";
    }

    public static class JwtClaims
    {
      public const string ApiAccess = "api_access";
    }
  }

  public static class SpecialListTypeConst
  {
    public const string TAX = "TAX";
    public const string TaxOthers = "Tax-others";
    public const string TaxCode = "TAX";
    public const string TaxOthersCode = "TO";
    public const string IRM = "IRM";
    public const string Valuation = "Valuation";
    public const string DataAnalytics = "Data Analytics";
    public const int ValueDefaultRateCYBudget = 40;
    public const int ValueDefaultRateCYBudget2 = 37;
  }

  public static class SettingKeyConst
  {
    public const string TAX_Function_Default_CC_For_Costing = "TAX_Function_Default_CC_For_Costing";
  }

  public static class ColumnFilterBudget
  {
    public const string Status = "Status";
    public const string Budgetgroup = "Budgetgroup";
    public const string TypeOfWork = "TypeOfWork";
    public const string Recurring = "Recurring";
    public const string YearEnd = "YearEnd";
    public const string ClientGroupName = "ClientGroupName";
    public const string PIE = "PIE";
    public const string Exempt = "Exempt";
    public const string NewClient = "NewClient";
    public const string LastYearOfAudit = "LastYearOfAudit";
    public const string BU = "BU";
    public const string EP = "EP";
    public const string EM = "EM";
    public const string CoEMs = "CoEMs";
    public const string EQCR = "EQCR";
    public const string EPIds = "EPIds";
    public const string EMIds = "EMIds";
    public const string CoEMsIds = "CoEMsIds";
    public const string EQCRIds = "EQCRIds";
    public const string ApprovalDate = "ApprovalDate";
    public const string LastModifiedBy = "LastModifiedBy";
    public const string LastModifiedDate = "LastModifiedDate";
    public const string KPMGFY = "KPMGFY";
    public const string APICBudget = "APICBudget";
  }

  public static class ColumnSortBudget
  {
    public const string Status = "budgetGroupStatus.name";
    public const string Budgetgroup = "budgetGroupName";
    public const string TypeOfWork = "typeOfWork.name";
    public const string Recurring = "recurring";
    public const string YearEnd = "yearEnd";
    public const string ClientGroupName = "clientGroupName";
    public const string PIE = "pie";
    public const string Exempt = "exempt.exemptName";
    public const string NewClient = "newClient";
    public const string LastYearOfAudit = "lastYearOfAudit";
    public const string BU = "bu";
    public const string EP = "ep";
    public const string EM = "em";
    public const string CoEMs = "coeMs";
    public const string EQCR = "eqcr.name";
    public const string ApprovalDate = "apicApprovalDate";
    public const string LastModifiedBy = "lastUpdatedBy.employeeName";
    public const string LastModifiedDate = "lastUpdatedDate";
    public const string KPMGFY = "fy";
    public const string APICBudget = "statusAPIC.name";
  }

  public static class SortType
  {
    public const string Desc = "desc";
    public const string Asc = "asc";
  }

  public static class GradeString
  {
    public const string Grades = ", P, PA1, D1S, AM, M, D1, D2, PA2, S3, S4, G1, G2, A3, A2, A1";
  }

  public static class ChargeOutRateConst
  {
    public const string Advisory = "Advisory";
    public const string Tax = "Tax";
    public const string Audit = "Audit";
  }

  public static class Log
  {
    public const string StartAuthenication = "Start Authenication";
    public const string BRPHostStart = "BRP host started ...";
    public const string BRpHostTerminated = "BRP host terminated unexpectedly.";
    public const string BRpHostClosed = "BRP host closed.";
    public const string StartGetUser = "Start GetUserRoles";
    public const string UserCalled = "get user role called";
    public const string AddUserCalled = "add user role called";
    public const string RemoveUserCalled = "remove user role called";
    public const string EmployeeCalled = "get employee called";
    public const string RoleCaled = "get role called";
    public const string SendEmailRemindPendingBGForEP = $"SendEmailRemindPendingBGForEP - 3";
  }

  public static class StartUp
  {
    public const int TimeSpanHours = 24;
    public const int TimeSpanDays = 30;
  }

  public static class SearchEmployeeType
  {
    public const string EQCR = "EQCR";
    public const string EP = "EP";
    public const string COEMS = "COEMS";
    public const string EM = "EM";
    public const string SO = "SO";
  }

  public static class TypeUpdate
  {
    public const string Apic = "APIC";
    public const string BU = "BU";
    public const string Portfolio = "Portfolio";
  }

  public static class CustomMessage
  {
    public const string MSG11 = "Specialist Hours added successfully";
    public const string MSG12 = "Specialist Hours updated successfully";
    public const string MSG18 = "This Specialist type already exists";
    public const string MSG18dot1 = "This subgroup is already tagged to an existing Budget Group";

    public const string AddTagingSubgroupSuccess =
      "Subgroup <b class = 'red_color'>{0}</b> tagged successfully";

    public const string MSG21 = "This Budget Group has been locked by APIC.";
    public const string MSG23 = "Budget group updated successfully";

    public const string MSG37 =
      "This Subgroup is the Primary Subgroup of another Budget Group. Please select a different Subgroup.";

    public const string MSG5 = "The budget group already exists";
    public const string AddBudgetGroupSuccess = "New record created successfully";
    public const string MSG42 = "This Subcontractor Cost already exists";
    public const string MSG35 = "Subcontractor Costs added successfully";
    public const string MSG34 = "Subcontractor Costs updated successfully";
    public const string MSG46 = "The Primary Entity is PIE.You can't de-select PIE for this Budget Group.";
    public const string MSG38 = "This Billing Schedule already exists";
    public const string MSG30 = "Billing Schedule added successfully";
    public const string MSG31 = "Billing Schedule updated successfully";
    public const string MSG61 = "Total Finalized Fee updated successfully";
    public const string MSG58 = "Budget Group(s) submitted successfully.";
    public const string MSG58S = "Budget Group(s) returned successfully.";
    public const string MSG62 = "You cannot approve Budget Groups that are not “Pending Approval”";
    public const string MSG62s = "You cannot reject Budget Groups that are not “Pending Approval”";
    public const string MSG66 = "Please select a valid Budget Group to submit to APIC";
    public const string MSG66s = "Budget groups Submit To APIC successfully";

    public const string MSG67 =
      "You cannot approve Budget Groups whose APIC status is different from “Submitted”";

    public const string MSG67S =
      "You cannot return Budget Groups whose APIC status is different from “Submitted”";

    public const string MSG56 = "Submit Successfully";
    public const string MSG53 = "Please select at least one Budget Group";
    public const string MSG63 = "Please select at least one Budget Group to approve";
    public const string MSG57 = "Budget groups Approved successfully";
    public const string MSG57s = "Budget groups Reject successfully";
    public const string MSG68 = "Budget Group has been accepted";
    public const string MSG49 = "New resource added successfully";

    public const string MSG59 =
      "</br>Audit CY Budget updated successfully. The Budget Group will be submitted to EP to re-approve " +
      "because the CY Budget Engagement Hours has been increased by more than 5%";

    public const string MSG15 = "Audit CY Budget updated successfully";
    public const string NotUpdate = "Can not update budget group with Pending Approval status";
    public const string MSG59dot1 = "Updated Budget Group has been submitted to EP to re-approve";
    public const string MSG76 = "Only Budget Group drafts can be deleted.";
    public const string MSG75 = "Please select at least one Budget Group";

    public const string MSG78 =
      "Primary Entity of this Budget Group was deleted in source system. Please replace with new Primary Entity";

    public const string MSG79 =
      "Primary Subgroup of this Budget Group was deleted in CRS. Please replace with new Primary Subgroup";

    public const string MSG69 = "Are you sure you want to delete?";
    public const string MSG80 = "This Budget Group is deleted by APIC. Please refresh the page to contitnue.";
    public const string InvalidState = "Invalid State";
    public const string ExpiredBGToSubmit = "Expired BG to submit";
    public const string NoTaggedEntity = "No Tagged Entity";
    public const string NoTaggedSubgroup = "No tagged Subgroup";
    public const string MissingCYBudgetManagement = "Missing CY Budget Management";
    public const string MissingAPIC = "Last year of audit ( to exclude from APIC Budget) is required";
    public const string MissingAccountingStandard = "Missing Accounting Standard";
    public const string MissingAudittingStandard = "Missing Auditting Standard";
    public const string MissingGrossFee = "Missing Gross Fee";
    public const string MissingBillingSchedule = "Missing Billing Schedule";
    public const string UnlockedBudgetGroup = "Budget group is not locked";
    public const string LockedBudgetGroup = "Budget group is already locked";
    public const string BudgetGroupDraftsCannotSubmitted = "Drafts cannot be submitted";

    public const string SubmitToAPICStatusError =
      "Budget Group is not Approved and BG APIC status is not blank or Returned";

    public const string ApproveOrRejectBudgetGroupStatusError = "Budget Group is not Pending Approval";
    public const string AcceptOrReturnBudgetGroupStatusError = "Budget Group APIC status is not Submitted";
    public const string BudgetLocked = "Budget Group locked. {0}({1}) is currently editing the Budget Group.";
    public const string WrongStatus = "Budget Group cannot be edited during Pending Approval";
  }

  public static class TemplateFile
  {
    public const string TEMPLATE_MY_PORTFOLIO = "Templates/TEMPLATE_MY_PORTFOLIO.xlsx";
    public const string TEMPLATE_TAGGING_ENTITIES = "Templates/TEMPLATE_TAGGING_ENTITIES.xlsx";
    public const string TEMPLATE_TAGGING_ENTITIES_REPONSE = "TEMPLATE_TAGGING_ENTITIES.xlsx";
    public const string TEMPLATE_MY_PORTFOLIO_REPONSE = "TEMPLATE_MY_PORTFOLIO.xlsx";
    public const string TEMPLATE_TAGGING_ENTITIES_RESPONSE = "TEMPLATE_TAGGING_ENTITIES.xlsx";
    public const string TEMPLATE_MY_PORTFOLIO_RESPONSE = "TEMPLATE_MY_PORTFOLIO.xlsx";
    public const string SheetFile = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    public const string MyPortfolioFile = "MY_PORTFOLIO";
    public const string TaggingEntityFile = "ENTITIES_ENGT_LIST";
    public const string BillingScheduleFile = "BILLING SCHEDULE";
    public const string FileExtend = ".xls";
    public const string FileExtendXLSX = ".xlsx";
    public const string Hyphen = "_";
    public const string TEMPLATE_BILLING_SCHEDULE = "Templates/Template_Billing_Schedule.xlsx";
    public const string TEMPLATE_BILLING_SCHEDULE_RESPONSE = "TEMPLATE_BILLING_SCHEDULE.xlsx";
    public const string ZipArchival = " ARCHIVAL_DATA_BRP_{0}_{1}.zip";
    public const string FileExtendCsv = ".csv";
    public const string WorkloadAssessmentOverview = "WORKLOAD_ASSESSMENT_OVERVIEW";
    public const string WorkloadAssessmentOverviewByMonth = "WORKLOAD_ASSESSMENT_OVERVIEW_BY_MONTH";
    public const string WorkloadAssessmentOverviewSheetName = "WORKLOAD ASSESSMENT BY MONTH";
    public const string BudgetOverview = "BUDGET GROUP OVERVIEW";
    public const string BudgetGroupForBUFile = "Draft_Reject_Engagement_Budgets";
    public const string AuditTrails = "AUDIT_TRAILS";
    public const string TaggingEntitiesEngtCodes = "Tagging Entities & Engt Codes";
    public const string ExportForecastBudgetReport = "Export Forecast & Budget report";
  }

  public static class EntityName
  {
    public const string BudgetGroups = "Budget Groups";
    public const string BudgetGroupsCoEMs = "Budget Groups CoEMs";
    public const string TaggingSubgroups = "Tagging Subgroups";
    public const string TaggingEntities = "Tagging Entities";
    public const string CYBudgetAuditHours = "CY Budget Audit Hours";
    public const string CYBudgetAuditHoursResources = "CY Budget Audit Hours Resources";
    public const string SpecialistHour = "Specialist Hour";
    public const string SpecialistHourDetails = "Specialist Hour Details";
    public const string CYForecastAuditHours = "CY Forecast Audit Hours";
    public const string AuditHoursReconciliation = "Audit Hours Reconciliation";
    public const string SubconCosts = "Subcon Costs";
    public const string NonBillableExpenses = "Non Billable Expenses";
    public const string GrossFees = "Gross Fees";
    public const string BillingSchedule = "Billing Schedule";
    public const string BillingScheduleDetail = "Billing Schedule Detail";
    public const string BudgetGroupsComments = "Budget Groups Comments";
    public const string BudgetGroupsHistories = "Budget Groups Histories";
    public const string GrossFeeReason = "Gross Fee Reasons";
    public const string BudgetAuditHour = "Budget Audit Hours";
    public const string BudgetGroupLock = "Budget Group Locks";
    public const string ChargeOutRate = "Charge Out Rates";
    public const string CrsBooking = "Crs Bookings";
    public const string CrsBookingRequest = "Crs Booking Requests";
    public const string CrsBookingDailyFactor = "Crs Booking Daily Factors";
    public const string CrsKeyDeadline = "Crs KeyDeadlines";
    public const string CrsSubgroup = "Crs Subgroups";
    public const string WipFees = "WipFees";
    public const string TimesheetHour = "Timesheet Hours";
  }

  public static class AuditHoursReconciliationConstants
  {
    public const string Audit = "Audit";

    public const string AuditHoursReconciliationConstantsNotExisted =
      "Audit Hours Reconciliation Constants Not Existed !";
  }

  public static class StatusResponse
  {
    public const string True = "True";
    public const string Fail = "Fail";
  }

  public static class ColumnHeaderImportBudgetGroup
  {
    public const string BudgetGroupIDHeader = "Budget Group ID";
    public const string SubgroupIDHeader = "Subgroup ID*";
    public const string PIEHeader = "PIE";
    public const string NewClientHeader = "New Client";
    public const string LastYearOfAuditHeader = "Last year of Audit";
    public const string EQCRHeader = "EQCR";
    public const string EPHeader = "EP*";
    public const string CoEMHeader = "Co-EM";
    public const string Remarks = "Remarks";
  }

  public static class ErrorMessageImport
  {
    public const string MissingRequiredField = "Missing required field ";
    public const string MissingRequiredFieldSubgroupID = "Subgroup ID";
    public const string MissingRequiredFieldEp = "EP";
    public const string MissingRequiredFieldEm = "Engagement Management ID";
    public const string MissingRequiredFieldEpId = "Engagement Partner ID";
    public const string MissingRequiredFieldBudgetGroupID = "Budget Group ID";
    public const string MissingRequiredFieldEntityId = "Entity ID";
    public const string MissingRequiredFieldTaxClassification = "Tax Classification";
    public const string MissingRequiredFieldCcy = "Ccy";
    public const string MissingRequiredFieldCurrentYearEngagementCode = "Current Year Engagement Code";
    public const string MissingRequiredFieldClientNo = "ClientNo";
    public const string MissingRequiredFieldAccountingStandard = "AccountingStandards";
    public const string MissingRequiredFieldAuditingStandard = "AuditingStandards";
    public const string InvalidBudgetGroupID = "Invalid Budget Group ID";
    public const string BudgetGrouplocked = "Budget Group is locked";
    public const string InvalidSubgroupID = "Invalid Subgroup ID";
    public const string InvalidSubgroup = "Invalid Subgroup";
    public const string InvalidEQCR = "Invalid EQCR";
    public const string InvalidCoEM = "Invalid Co-EM";
    public const string InvalidEP = "Invalid EP";
    public const string Duplicate = "Duplicate";
    public const string InvalidData = "Invalid data ";
    public const string InvalidPyEngagementCode = "Invalid PY Engagement Code";
    public const string InvalidCyEngagementCode = "Invalid CY Engagement Code";
    public const string InvalidEntityID = "Invalid Entity ID";
    public const string ValidatePie = "Primary entity PIE is Yes, unable to switch Budget Group PIE to No";
    public const string BudgetExists = "Budget already exists";
    public const string Valid = "Valid";
    public const string Successfully = "Successfully";
    public const string PrimaryTaggedEntityNull = "Primary Tagged Entity is null";
    public const string WrongFileFormat = "Wrong file format. Please upload a valid file";
    public const string TaxClassificationNotExist = "Tax Classification value must exist in the system";
    public const string CcyNotExist = "CCY must exist in the system";
    public const string EntityErrorTag = "The Entity has not been tagged to the current Budget Group";
    public const string ValidReason = "Valid Reason for lost client is required";
    public const string BudgetGroupIDDoNotMatch = "Budget Group ID does not match";
    public const string BudgetGroupIsEditting = "Can not import this Budget group. Somebody is editing";
    public const string BudgetGroupIsPendingApproval = "Budget Group is pending approval";
    public const string ImportFail = "Import fail please check template file";
    public const string MissingRequiredFieldCRSSubgroupId = "CRSSubgroupId";
    public const string MissingRequiredFieldYearEnd = "YearEnd";
    public const string BuDoesNotExist = "BU does not exist";
    public const string InvalidEmployee = " invalid employee ID";
    public const string ClientNoDoesNotExist = "Client No does not exist";
    public const string InvalidAccountingStandards = "Invalid Accounting Standards";
    public const string InvalidAuditingStandards = "Invalid Auditing Standards";
    public const string InvalidExemptId = "Invalid ExemptId";
  }

  public static class ColumnHeaderImportTaggingEntities
  {
    public const string BudgetGroupIDHeader = "Budget Group ID*";
    public const string EntityIdHeader = "Entity ID*";
    public const string EntityYearEndHeader = "Entity Year End";
    public const string NewClientHeader = "New Client";
    public const string LostClientHeader = "Lost Client";
    public const string ReasonForLostClientHeader = "Reason for Lost Client";
    public const string CurrentYearEngagementCodeHeader = "Current Year Engagement Code";
    public const string PriorYearEngagementCodeHeader = "Prior Year Engagement Code";
    public const string AccountingStandardsHeader = "Accounting Standards";
    public const string AuditingStandardsHeader = "Auditing Standards";
  }

  public static class ColumnHeaderImportBillingSchedule
  {
    public const string EntityIdHeader = "Entity ID*";
    public const string BillingPartyHeader = "Billing party";
    public const string TaxClassificationHeader = "Tax classification*";
    public const string CcyHeader = "Ccy*";
    public const string JanHeader = "Jan";
    public const string FebHeader = "Feb";
    public const string MarHeader = "Mar";
    public const string AprHeader = "Apr";
    public const string MayHeader = "May";
    public const string JunHeader = "Jun";
    public const string JulHeader = "Jul";
    public const string AugHeader = "Aug";
    public const string SepHeader = "Sep";
    public const string OctHeader = "Oct";
    public const string NovHeader = "Nov";
    public const string DecHeader = "Dec";
  }

  public static class BudgetGroupAction
  {
    public const string Approve = "Approve";
    public const string Reject = "Reject";
  }

  public static class Number
  {
    public const int Zero = 0;
    public const int One = 1;
    public const int Two = 2;
    public const int Three = 3;
    public const int Four = 4;
    public const int Five = 5;
    public const int Six = 6;
    public const int Seven = 7;
    public const int Eight = 8;
    public const int Nine = 9;
    public const int Ten = 10;
    public const int Eleven = 11;
    public const int Twelve = 12;
    public const int Thirteen = 13;
    public const int Fourteen = 14;
    public const int Fifteen = 15;
    public const int Sixteen = 16;
    public const int Seventeen = 17;
    public const int Eighteen = 18;
    public const int Nineteen = 19;
    public const int Twenty = 20;
    public const int Mil = 1000000;
  }

  public static class EmailTemplate
  {
    public const string RemindPendingApproval = "RemindPendingForApprovalBudgetGroup";
    public const string RemindApprovedAndReject = "RemindApprovedAndReject";
    public const string RequestDeleteBudgetGroups = "RequestDeleteBudgetGroups";
    public const string SubmitBGtoAPICforEP = "SubmitBGtoAPICforEP";
    public const string ArchivalDataForApic = "ArchivalDataForApic";
    public const string RemindAPICNewBG = "RemindAPICNewBG";
    public const string RemindUpdateBGForEP = "RemindUpdateBGForEP";
    public const string RemindBudgetGroupIsLostClient = "RemindBudgetGroupIsLostClient";
    public const string PendingRollForwardToManager = "PendingRollForwardToManager";
    public const string PendingRollForwardToBuCoo = "PendingRollForwardToBuCoo";
    public const string DraftRejectBudgetsAttachmentForBU = "DraftRejectBudgetsAttachmentForBU";
  }

  public static class PageUrl
  {
    public const string MyPortfolio = "my-portfolio";
  }

  public static class TypeLock
  {
    public const string Lock = "Lock";
    public const string UnLock = "Unlock";
    public const string Check = "Check";
    public const string UnLockAll = "UnLockAll";
  }

  public static class ResourceGrade
  {
    public const string P = "P";
    public const string PA1 = "PA1";
    public const string PA2 = "PA2";
    public const string D1S = "D1S";
    public const string D1 = "D1";
    public const string D2 = "D2";
    public const string M = "M";
  }

  public static class RollForwardMessage
  {
    public const string BudgetGroupDoesNotSatisfyCondition =
      "Please select an Approved Budget Group that is recurring and is not a Lost Client to roll forward.";

    public const string CanNotRollForwardRolling =
      "Can not roll forward this Budget group. Somebody is rolling";

    public const string BudgetGroupAlreadyExists = "Budget Group already exists";
    public const string FailToCreateSubgroup = "Fail to create Subgroup";
    public const string NoteHistory = "Roll forward from Budget Group ID: ";

    public const string CanNotRollForwardEditing =
      "Cannot roll forward this Budget Group because another user is editing it.";
  }

  public static class ColumnFilterWorkLoad
  {
    public const string FY = "FY";
    public const string Grade = "Grade";
    public const string BusinessUnitName = "BU";
    public const string EmployeeName = "Name";
    public const string TargetChargeableHours = "targetChargeableHours";
    public const string BudgetChargeableHours = "budgetchargeableHours";
    public const string TargetBudgetVariance = "targetBudgetVariance";
    public const string CYActualYTDHours = "cYActualYTDHours";
    public const string TargetCYActualYTDVariance = "targetCYActualYTDVariance";
  }

  public static class StatusCheckPortfolio
  {
    public const int IsSuccess = 0;
    public const int TaggingSubgroup = 1;
    public const int TaggingEntity = 2;
  }

  public static class ActionName
  {
    public const string AddBudget = "Add BudgetGroup";
    public const string UpdateBudget = "Update BudgetGroup";
    public const string DeleteBudget = "Delete BudgetGroup";
    public const string UpdateBudgetForAPIC = "Update Budget for APIC";
    public const string UpdateTotalFinalisedFee = "Update Total Finalised Fee";
    public const string EngagementHoursAudit = "Save Audit In Engagement Hours";
    public const string ReportingFrameWork = "Save Reporting FrameWork";
    public const string LockUnLockBudgetGroup = "Lock Or UnLock BudgetGroup";
    public const string UnLockBudgetGroup = "UnLock BudgetGroup";
    public const string LockBudgetGroup = "Lock BudgetGroup";
    public const string ImportBudget = "Import Budget";
    public const string AddTaggingEntity = "Add Tagging Entity";
    public const string AddTaggingSubGroup = "Add Tagging SubGroup";
    public const string DeleteCoEMs = "Delete CoEMs";
    public const string AddCoEMs = "Add CoEMs";
    public const string UpdateTaggingEntity = "Update TaggingEntity";
    public const string DeleteTaggingEntity = "Delete TaggingEntity";
    public const string UpdateTaggingSupGroup = "Update TaggingSupGroup";
    public const string DeleteTaggingSubGroup = "Delete TaggingSubGroup";
    public const string SaveGrosFee = "Save GrosFee";
    public const string DeleteGrossFee = "Delete GrossFee";
    public const string AddGrossFee = "Add GrossFee";
    public const string UpdateCYForecast = "Update CY Forecast";
    public const string UpdateAuditCYBudget = "Update Audit CYBudget";
    public const string AddAuditCYBudget = "Add Audit CYBudget";
    public const string AddSpecialistHour = "Add Specialist Hour";
    public const string UpdateSpecialistHour = "Update Specialist Hour";
    public const string DeleteSpecialistHour = "Delete Specialist Hour";
  }
}