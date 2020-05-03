import { Component, Output, EventEmitter } from '@angular/core';
import { NgbDateStruct, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'filter-performance',
  template: `<div style="display: flex;">
  <input mdbInput type="text" name="text" style="width: 400px;" (input)="textChange($event.target.value)" [(ngModel)]="text" id="form1" class="form-control"> &nbsp;&nbsp;&nbsp;&nbsp;

<div class="form-group">
    <div class="input-group">
    <input class="form-control" placeholder="yyyy-mm-dd"
             name="dp" [(ngModel)]="model" (ngModelChange)="dateChange()" ngbDatepicker #d="ngbDatepicker" style="width: 150px;">
    <div class="input-group-append">
        <button class="btn btn-outline-secondary calendar" (click)="d.toggle()" type="button"></button>
      </div>
  </div>
</div>
&nbsp;&nbsp;&nbsp;&nbsp;
<button type="button" (click)="resetFilters()" style="height: 37px;" class="btn btn-info">сбросить фильтр</button>
</div>`
})
export class PerformanceFilter {
  @Output()
  textEv: EventEmitter<string> = new EventEmitter<string>();

  @Output()
  dateEv: EventEmitter<Date> = new EventEmitter<Date>();

  @Output()
  clearEv: EventEmitter<any> = new EventEmitter<any>();

  text: string;

  model: NgbDateStruct;
  date: any;//{ year: number, month: number, day: number };

  constructor(private calendar: NgbCalendar) {
  }

  
  textChange(val) {
    this.textEv.emit(val);
  }

  dateChange() {
    this.dateEv.emit(new Date(this.model.year, this.model.month - 1, this.model.day));
  }

  resetFilters() {
    this.text = '';
    this.model = null;
    this.clearEv.emit(null);
  }
}



