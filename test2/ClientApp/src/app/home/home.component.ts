import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: [`
    .performance-list{
      display: flex;
      flex-wrap: wrap;
      justify-content: space-between;
    }
  `]
})
export class HomeComponent {
}
