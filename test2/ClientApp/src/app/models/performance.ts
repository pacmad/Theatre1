import { PerformanceDate } from './performanceDate';

export class Performance {
  id: string;
  name: string;
  description: string;
  image: string;
  performanceDates: PerformanceDate[];
}
