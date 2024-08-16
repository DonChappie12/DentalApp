import { LoginResponse } from './../../../model/response/loginResponse';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, retry, throwError } from 'rxjs';

// const baseUrl: string = "http://localhost:44305/api/Auth/";
// const baseUrl: string = "http://localhost:7181/api/Auth/";
const baseUrl: string = "http://localhost:5230/api/auth/";

@Injectable({
  providedIn: 'root'
})

export class AuthserviceService {
  constructor(private http: HttpClient) { }

  loginUser<LoginResponse>(loginCreds: any){
  // loginUser(loginCreds: LoginViewModel){
    // console.log(loginCreds)
    // TODO Have this method pass the correct URL and body
    return this.http.post(baseUrl + "login", loginCreds)
    // .pipe(
    //   // * This is to retry if needed
    //   // retry(3),
    //   catchError(this.handleError)
    // )
    // .subscribe((res: any) => {
    //   console.log(res.jwtToken)
    //   console.log(res.refreshToken)
    // })
  }

  logoutUser(){}

  registerUser(){}

  refreshToken(){}

  private handleError(error: HttpErrorResponse){
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else if(error.status === 400) {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      // return `Backend returned code ${error.status}, body was: ` + error.error
      console.error(`Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}
