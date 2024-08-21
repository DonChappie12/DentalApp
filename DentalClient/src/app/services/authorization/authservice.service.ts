import { LoginResponse } from './../../../model/response/loginResponse';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, retry, throwError } from 'rxjs';

// const baseUrl: string = "http://localhost:44305/api/Auth/";
// const baseUrl: string = "http://localhost:7181/api/Auth/";
const baseUrl: string = "http://localhost:5230/api/auth/";

@Injectable({
  providedIn: 'root'
})

export class AuthserviceService {
  isLoginSubject = new BehaviorSubject<boolean>(this.hasToken());
  constructor(private http: HttpClient) { }

  loginUser<LoginResponse>(loginCreds: any){
    // TODO Have this method pass the correct URL and body
    return this.http.post(baseUrl + "login", loginCreds)
  }

  logoutUser(): void {
    localStorage.removeItem("token")
    localStorage.removeItem("refresh")
    this.isLoginSubject.next(false)
  }

  registerUser(){}

  refreshToken(){}

  setStorage(data: any): void {
    localStorage.setItem("token", data.jwtToken)
    localStorage.setItem("refresh", data.refreshToken)
    this.isLoginSubject.next(true)
  }

  isLoggedIn(): Observable<boolean>{
    console.log(`isLoggedIn Function ${this.isLoginSubject.asObservable()}`)
    return this.isLoginSubject.asObservable()
  }

  private hasToken(): boolean{
    return !!localStorage.getItem("token")
  }

  // ? Not used currently but most likely
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
