export class ApiCallConstant {
  public static readonly BASE_URL = 'http://localhost:5062/api/';

  //Area name
  public static readonly AREA_AUTHENTICATION = this.BASE_URL + 'authentication';

  //for authentication
  public static readonly LOGIN_URL = this.AREA_AUTHENTICATION + '/login';

  public static readonly FORGOT_PASSWORD_URL =
    this.AREA_AUTHENTICATION + '/forgot-password';

  public static readonly SUBMIT_REGISTER_PATIENT_REQUEST =
    this.AREA_AUTHENTICATION + '/submit-register-patient-request';
}
