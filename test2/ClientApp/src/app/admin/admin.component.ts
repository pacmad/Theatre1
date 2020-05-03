import { Component, OnInit } from '@angular/core';
import { Router, Route, ActivatedRoute } from '@angular/router';
import { Performance } from './../models/performance';
import { PerformanceService } from './../services/performanceService';
import { NgbDateStruct, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'admin',
  templateUrl: 'admin.component.html',
  styleUrls: ['admin.component.scss']
})
export class CreateEdit implements OnInit {
  model: Performance = new Performance();
  id: string;
  test: string;
  constructor(private route: ActivatedRoute, private router: Router, private service: PerformanceService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.service.getItem(this.id)
        .subscribe(item => {
          for (var i = 0; i < item.performanceDates.length; ++i) {
            item.performanceDates[i].date = new Date(item.performanceDates[i].date.toString());
          }
          this.model = item
        });
    });
  }

  editDate(ev, id) {
    var arr = ev.split('-');
    for (var i = 0; i < this.model.performanceDates.length; ++i) {
      if (this.model.performanceDates[i].id == id) {
        this.model.performanceDates[i].date = new Date(arr[0], arr[1], arr[2]);
      }
    }
  }

  save() {
    // валидация введенных данных
    this.service.edit(this.model)
      .subscribe(res => console.log(res),
                 err => console.log(err));
  }
}

