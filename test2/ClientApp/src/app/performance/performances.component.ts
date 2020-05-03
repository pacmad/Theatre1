import { Performance } from './../models/performance';
import { PerformanceModel } from './../models/PerformanceModel';
import { Component, OnInit } from '@angular/core';
import { PerformanceService } from './../services/performanceService';

@Component({
  selector: 'performances-list',
  template: `<filter-performance
                      class="col col-md-12"
                      (textEv)='filterByName($event)'
                      (dateEv)='filterByDate($event)'
                      (clearEv)='clearFilter($event)'>
             </filter-performance><br/>
            <performance-item
                  *ngFor="let item of viewList | slice: (page-1) * 6 : (page-1) * 6 + 6"
                  class="item-container"
                  [model]="item">
             </performance-item>`,
  styleUrls: ['performances.component.scss']
})
export class PerformancesList implements OnInit {
  performances: Performance[] = [];
  total: number = 0;
  page: number = 1;
  pageSize: number = 6;
  viewList: Performance[] = [];

  constructor(private service: PerformanceService) { }

  ngOnInit(): void {

    //this.service.getPerformances(this.page, 10)
      //.subscribe((data: PerformanceModel) => {
      //  this.performances = data.performances;
      //  this.total = data.total;
      //},
      //  error => console.log(error)
      //);

    this.service.getAll()
      .subscribe(
        (data: Performance[]) => {
          this.performances = data;
          this.total = data.length;
          this.viewList = data
        },
        error => console.log(error)
      );
  }

  filterByName($event: string) {
    //debugger;
    this.page = 1;
    this.viewList = this.viewList.filter(item =>
      item.name.toLowerCase().indexOf($event.toLowerCase()) != -1
    )

    this.total = this.viewList.length;
  }

  filterByDate($event: Date) {
    this.page = 1;
    this.viewList = this.viewList.filter(item => {
      if (!item.performanceDates || !item.performanceDates.length)
        return false;
      for (var i = 0; i < item.performanceDates.length; ++i) {
        var date = new Date(item.performanceDates[i].date.toString());
        if (date.getFullYear() == $event.getFullYear() && date.getMonth() == $event.getMonth() && date.getDate() == $event.getDate())
          return item;
      }
    }
    )
    this.total = this.viewList.length;
  }

  clearFilter($event) {
    this.page = 1;
    this.viewList = this.performances;
    this.total = this.performances.length;
  }

}
