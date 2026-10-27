export interface Novedad {
  id: number;
  titulo: string;
  resumen: string;
  contenido: string;
  imagenUrl: string | null;
  estaPublicada: boolean;
  creadaEn: string;
}

export type NovedadRequest = Omit<Novedad, 'id' | 'creadaEn'>;
