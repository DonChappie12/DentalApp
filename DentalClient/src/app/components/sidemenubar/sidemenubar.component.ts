import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { Router } from '@angular/router';

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
  isLoggedIn: boolean = false;

  constructor(private router: Router) {}

  ngOnInit() {
    this.menuItems = [
      {
        label: 'Main Page',
        routerLink: ['/Dashboard']
      },
      {
        label: 'Front Desk',
        items: [
          {
            label: 'Create an Appoinment',
            routerLink: ['/appointment-creation']
          },
          {
            label: 'Calendar',
            routerLink: ['/calendar']
          }
        ]
      },
      {
        label: 'Doctors',
        items: [
          {
            label: 'Schedule',
            routerLink: ['/random-endpoint']
          },
          {
            label: 'Special Cases',
            routerLink: ['/random-endpoint']
          },
          {
            label: 'Surgery',
            routerLink: ['/random-endpoint']
          }
        ]
      },
      {
        label: 'Higenist',
        items: [
          {
            label: 'Schedule',
            routerLink: ['/random-endpoint']
          },
          {
            label: 'Special Cases',
            routerLink: ['/random-endpoint']
          }
        ]
      },
      {
        label: 'Assistants',
        items: [
          {
            label: 'Schedule',
            routerLink: ['/random-endpoint']
          }
        ]
      }
    ]
  }

  RedirectToLogin(urn: string){
    this.router.navigate([`${urn}`])
  }

  OpenLoginDialog(){
    console.log("Clicked on Login Dialog")
  }

}
