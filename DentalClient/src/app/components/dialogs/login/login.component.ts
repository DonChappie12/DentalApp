import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { PasswordModule } from 'primeng/password';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputTextModule } from 'primeng/inputtext';
import { FormGroup, FormControl, FormBuilder, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms'
import { AuthserviceService } from '../../../services/authorization/authservice.service';
import { LoginResponse } from '../../../../model/response/loginResponse';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs';
import { ErrorHandlerService } from '../../../services/error-handler/error-handler.service';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { CommonModule } from '@angular/common';
import { MessagesModule } from 'primeng/messages';
import { Message } from 'primeng/api';
// import { LoginViewModel } from '../../../../model/viewModel/loginViewModel';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ButtonModule,
    InputTextModule,
    ReactiveFormsModule,
    FormsModule,
    PasswordModule,
    InputGroupModule,
    InputGroupAddonModule,
    ProgressSpinnerModule,
    MessagesModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginCredentials: FormGroup;
  error: string = '';
  isProgressBarVisible: boolean = false;
  errorMessages: Message[] = [];

  constructor(
    public ref: DynamicDialogRef,
    private formBuilder: FormBuilder,
    private auth: AuthserviceService,
    private errorHandler: ErrorHandlerService
  ) {
    this.loginCredentials = this.formBuilder.group({
      alias: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  SubmitCredentials() {
    this.isProgressBarVisible = true;
    console.log("Submitting Creds")
    this.auth.loginUser<LoginResponse>(this.loginCredentials.value)
    .subscribe({
      next: (data: any) => {
      this.auth.setStorage(data)
      this.closeDialog()
    },
    error: (error: HttpErrorResponse) => {
      // console.log(error)
      // console.log('Error: ' + error.error)
      // TODO Investigate which would be a better option to handle error

      // catchError(error.error)
      this.error = this.errorHandler.handle(error)

      this.errorMessages = [
        {severity: 'error', detail: this.error}
      ]
      console.log(this.error)
    }});
    // TODO Have a spinner or something to let client know that it is trying to sign in
    this.isProgressBarVisible = false;
  }

  closeDialog(){
    console.log("Closing from login component")
    this.ref.close()
  }

}
