import { Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { CreateAppointmentComponent } from './components/create-appointment/create-appointment.component';
import { CalendarComponent } from './components/calendar/calendar.component';
// import { LoginComponent } from './components/login/login.component';

export const routes: Routes = [
  {path: 'dashboard', component: DashboardComponent},
  {path: 'appointment-creation', component: CreateAppointmentComponent},
  {path: 'calendar', component: CalendarComponent},
  // {path: 'login', component: LoginComponent},
  {path: '404-page-not-found', component: PageNotFoundComponent},
  {path: '', redirectTo: '/dashboard', pathMatch: 'full'},
  { path: '**', redirectTo:'404-page-not-found' },
];
