import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ErrorHandlerService {
  handle(error: HttpErrorResponse){
    switch(error.status){
      case 0:
        // throwError(() => new Error('Not connected'))
        return "Not connected"
      case 400:
        // throwError(() => new Error('Username or Password are incorrect'))
        return "Username or Password are incorrect"
      case 401:
        // throwError(() => new Error('Not logged in'))
        return "Not logged in"
      case 403:
        // throwError(() => new Error('Not authorized'))
        return "Not authorized"
      case 500:
        // throwError(() => new Error('Internal Server Error. Contact Help Desk'))
        return "Internal Server Error. Contact Help Desk"
      default:
        // throwError(() => new Error('Not connected'))
        return "I'm broken and don't know what to do"
    }
    // console.error('An error occurred:', error);
    // throwError(() => new Error('Something went wrong'));
  }
}
