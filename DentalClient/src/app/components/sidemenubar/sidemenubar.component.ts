import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { Router } from '@angular/router';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { TestDialogComponent } from '../dialogs/test-dialog.component';
import { LoginComponent } from '../dialogs/login/login.component';
import { AuthserviceService } from '../../services/authorization/authservice.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-sidemenubar',
  standalone: true,
  imports: [
    PanelMenuModule,
    MenubarModule,
    ButtonModule,
    InputTextModule,
    CommonModule,
    DialogModule
  ],
  templateUrl: './sidemenubar.component.html',
  styleUrl: './sidemenubar.component.css'
})

export class SidemenubarComponent {
  menuItems: MenuItem[] = [];
  isLoggedIn: Observable<boolean>;
  ref: DynamicDialogRef | undefined;

  constructor(
    private router: Router,
    public dialogService: DialogService,
    private authService: AuthserviceService
  ) {
    this.isLoggedIn = authService.isLoggedIn();
  }

  ngOnInit() {
    this.menuItems = [
      {
        label: 'Main Page',
        // route: '/dashboard'
        routerLink: ['/dashboard']
      },
      {
        label: 'Front Desk',
        items: [
          {
            label: 'Create an Appoinment',
            // route: '/appointment-creation'
            routerLink: ['/appointment-creation']
          },
          {
            label: 'Calendar',
            // route: '/calendar'
            routerLink: ['/calendar']
          }
        ]
      },
      {
        label: 'Doctors',
        items: [
          {
            label: 'Schedule',
            // route: '/random-endpoint'
            routerLink: ['/random-endpoint']
          },
          {
            label: 'Special Cases',
            // route: '/random-endpoint'
            routerLink: ['/random-endpoint']
          },
          {
            label: 'Surgery',
            // route: '/random-endpoint'
            routerLink: ['/random-endpoint']
          }
        ]
      },
      {
        label: 'Higenist',
        items: [
          {
            label: 'Schedule',
            // route: '/random-endpoint'
            routerLink: ['/random-endpoint']
          },
          {
            label: 'Special Cases',
            // route: '/random-endpoint'
            routerLink: ['/random-endpoint']
          }
        ]
      },
      {
        label: 'Assistants',
        items: [
          {
            label: 'Schedule',
            // route: '/random-endpoint'
            routerLink: ['/random-endpoint']
          }
        ]
      }
    ]
  }

  OpenLoginDialog(){
    this.ref = this.dialogService.open(LoginComponent, {
      header: 'Login',
      width: '50vw',
      modal:true,
      breakpoints: {
          '960px': '75vw',
          '640px': '90vw'
      },
    })

    // TODO Pass data to reflect if successful login variable isLoggedIn is true but reflecting on front end

  }

  logOutUser(){
    console.log("Logged out")
    this.authService.logoutUser();
  }

}
