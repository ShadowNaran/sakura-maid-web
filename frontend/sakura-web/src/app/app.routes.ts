import { Routes } from '@angular/router';
import { Admin } from './admin/admin';
import { Categorias } from './categorias/categorias';
import { Eventos } from './eventos/eventos';
import { Home } from './home/home';
import { Novedades } from './novedades/novedades';
import { ProductoDetalle } from './producto-detalle/producto-detalle';
import { Productos } from './productos/productos';
import { SobreNosotros } from './sobre-nosotros/sobre-nosotros';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'productos', component: Productos },
  { path: 'productos/:id', component: ProductoDetalle },
  { path: 'categorias', component: Categorias },
  { path: 'eventos', component: Eventos },
  { path: 'novedades', component: Novedades },
  { path: 'sobre-nosotros', component: SobreNosotros },
  { path: 'admin', component: Admin },
  { path: '**', redirectTo: '' },
];
