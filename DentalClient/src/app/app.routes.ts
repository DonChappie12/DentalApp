import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { CreateAppointmentComponent } from './create-appointment/create-appointment.component';
import { CalendarComponent } from './calendar/calendar.component';

export const routes: Routes = [
  {path: 'dashboard', component: DashboardComponent},
  {path: 'appointment-creation', component: CreateAppointmentComponent},
  {path: 'calendar', component: CalendarComponent},
  // {path: '404-page-not-found', component: PageNotFoundComponent},
  {path: '', redirectTo: '/dashboard', pathMatch: 'full'},
  { path: '**', component: PageNotFoundComponent },
];
