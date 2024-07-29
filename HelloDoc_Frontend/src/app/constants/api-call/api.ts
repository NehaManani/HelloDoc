export class ApiCallConstant {
  public static readonly BASE_URL = 'http://localhost:5062/api/';

  //Area name
  public static readonly AREA_AUTHENTICATION = this.BASE_URL + 'authentication';
  public static readonly AREA_ADMIN = this.BASE_URL + 'admin';

  //for authentication
  public static readonly LOGIN_URL = this.AREA_AUTHENTICATION + '/login';
  public static readonly VERIFY_OTP_URL =
    this.AREA_AUTHENTICATION + '/verify-otp';
  public static readonly RESEND_OTP_URL =
    this.AREA_AUTHENTICATION + '/resend-otp';
  public static readonly FORGOT_PASSWORD_URL =
    this.AREA_AUTHENTICATION + '/forgot-password';

  public static readonly RESET_PASSWORD_URL =
    this.AREA_AUTHENTICATION + '/reset-password';

  public static readonly SUBMIT_REGISTER_PATIENT_REQUEST =
    this.AREA_AUTHENTICATION + '/register-patient-request';

  public static readonly SUBMIT_REGISTER_PROVIDER_REQUEST =
    this.AREA_AUTHENTICATION + '/register-provider-request';

  //for admin
  public static readonly GET_PATIENT_LIST =
    this.AREA_ADMIN + '/patient-request-list';

  public static readonly GET_STATUS_COUNT_LIST =
    this.AREA_ADMIN + '/status-count-list';

  public static readonly GET_PATIENT_DETAILS =
    this.AREA_ADMIN + '/get-patient-details';

  public static readonly BLOCK_CASE = this.AREA_ADMIN + '/block-user-case';
}
