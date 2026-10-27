import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CAFE_CONFIG } from '../core/config/cafe.config';

@Component({
  imports: [RouterLink],
  selector: 'app-sobre-nosotros',
  styleUrl: './sobre-nosotros.css',
  templateUrl: './sobre-nosotros.html',
})
export class SobreNosotros {
  readonly cafe = CAFE_CONFIG;
}
