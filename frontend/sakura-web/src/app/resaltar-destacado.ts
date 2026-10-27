import { Directive, ElementRef, inject, input } from '@angular/core';

@Directive({
  selector: '[appResaltarDestacado]',
  host: {
    '(mouseenter)': 'resaltar()',
    '(mouseleave)': 'quitarResaltado()',
  },
})
export class ResaltarDestacado {
  private readonly elemento = inject(ElementRef<HTMLElement>);
  appResaltarDestacado = input(false);

  resaltar(): void {
    if (!this.appResaltarDestacado()) {
      return;
    }

    this.elemento.nativeElement.style.backgroundColor = '#fff0f6';
  }

  quitarResaltado(): void {
    this.elemento.nativeElement.style.backgroundColor = '';
  }
}
