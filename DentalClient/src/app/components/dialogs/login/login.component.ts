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


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ButtonModule,
    InputTextModule,
    ReactiveFormsModule,
    FormsModule,
    PasswordModule,
    InputGroupModule,
    InputGroupAddonModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginCredentials;

  constructor(public ref: DynamicDialogRef, private formBuilder: FormBuilder, private auth: AuthserviceService) {
    this.loginCredentials = this.formBuilder.group({
      alias: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  SubmitCredentials() {
    console.log("Submitting Creds")
    this.auth.loginUser<LoginResponse>(this.loginCredentials.value)
    // .subscribe((res: any) => {
    //   console.log(res.jwtToken)
    // })
    .subscribe({
      next: (data: any) => {
      console.log('Data: ' + data.jwtToken)
    },
    error: (error: HttpErrorResponse) => {
      console.log('Error: ' + error.error)
    }});
    // console.log(res)
    // TODO Have a spinner or something to letclient know that it is trying to sign in

    this.closeDialog()
  }

  closeDialog(){
    this.ref.close()
  }

}
