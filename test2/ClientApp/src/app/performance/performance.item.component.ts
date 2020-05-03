import { Component, Input } from '@angular/core';
import { Performance } from './../models/performance';


@Component({
  selector: 'performance-item',
  templateUrl:'performance.item.component.html',
  styleUrls: ['performance.item.component.scss']
})
export class PerformanceItem {
  @Input()
  model: Performance;
}

