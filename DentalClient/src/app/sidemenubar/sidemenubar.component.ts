import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-sidemenubar',
  standalone: true,
  imports: [PanelMenuModule, MenubarModule, ButtonModule, InputTextModule],
  templateUrl: './sidemenubar.component.html',
  styleUrl: './sidemenubar.component.css'
})

export class SidemenubarComponent {
  menuItems: MenuItem[] = [];

  ngOnInit() {
    this.menuItems = [
      {
        label: 'Test',
        items: [
          {
            label: 'Sub'
          }
        ]
      },
      {
        label: 'Test2',
        items: [
          {
            label: 'Sub'
          }
        ]
      },
      {
        label: 'Test3',
        items: [
          {
            label: 'Sub'
          }
        ]
      },
      {
        label: 'Test4',
        items: [
          {
            label: 'Sub'
          }
        ]
      }
    ]
  }

}
