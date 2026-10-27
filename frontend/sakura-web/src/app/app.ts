import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { CAFE_CONFIG } from './core/config/cafe.config';

@Component({
  imports: [RouterLink, RouterOutlet],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  readonly cafe = CAFE_CONFIG;
}
