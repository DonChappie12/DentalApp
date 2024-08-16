import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidemenubarComponent } from './components/sidemenubar/sidemenubar.component';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { BrowserModule } from '@angular/platform-browser';
// import { ɵBrowserAnimationBuilder } from '@angular/animations';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

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
