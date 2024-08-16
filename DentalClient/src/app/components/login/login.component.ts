import { Component } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { FormGroup, FormControl, FormBuilder, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { AuthserviceService } from '../../services/authorization/authservice.service';
import { LoginViewModel } from '../../../model/viewModel/loginViewModel';
import { LoginResponse } from '../../../model/response/loginResponse';
import { HttpErrorResponse } from '@angular/common/http';
// import { FloatLabelModule } from 'primeng/floatlabel';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    InputTextModule,
    ReactiveFormsModule,
    FormsModule,
    ButtonModule,
    PasswordModule,
    InputGroupModule,
    InputGroupAddonModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginCredentials;
  constructor(private formBuilder: FormBuilder, private auth: AuthserviceService) {
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
  }
}
