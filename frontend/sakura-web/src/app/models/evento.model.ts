export type EstadoEvento = 'Published' | 'Cancelled';

export interface Evento {
  id: number;
  titulo: string;
  descripcion: string;
  iniciaEn: string;
  ubicacion: string | null;
  aficheUrl: string | null;
  estado: EstadoEvento;
}

export type EventoRequest = Omit<Evento, 'id'>;
