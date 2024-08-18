import { Component, ElementRef } from '@angular/core';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-test-dialog',
  standalone: true,
  imports: [DialogModule],
  templateUrl: './test-dialog.component.html',
  styleUrl: './test-dialog.component.css'
})
export class TestDialogComponent {

  isVisible: boolean = false;

  constructor() {}

  showDialog(){
    console.log("Inside Test Dialog Component")
    this.isVisible = true
  }

}
