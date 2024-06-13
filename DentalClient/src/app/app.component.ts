import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidemenubarComponent } from './sidemenubar/sidemenubar.component';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SidemenubarComponent, CommonModule, InputTextModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'DentalClient';
}
