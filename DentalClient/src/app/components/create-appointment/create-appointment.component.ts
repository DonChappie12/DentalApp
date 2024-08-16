import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-appointment',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './create-appointment.component.html',
  styleUrl: './create-appointment.component.css'
})
export class CreateAppointmentComponent {
  value: string | undefined;
}
