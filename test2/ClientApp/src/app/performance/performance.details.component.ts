import { Component, OnInit } from '@angular/core';
import { Router, Route, ActivatedRoute } from '@angular/router';
import { Performance } from './../models/performance';
import { PerformanceService } from './../services/performanceService';
import { AuthorizeService } from './../../api-authorization/authorize.service';
import { RoleService } from './../services/roleService';

@Component({
  selector: 'performance-detail',
  templateUrl: 'performance.details.component.html',
  styleUrls: ['performance.details.component.scss']
})
export class PerformanceDetail implements OnInit {
  constructor(private activatedRoute: ActivatedRoute,
              private service: PerformanceService,
              private authService: AuthorizeService,
              private roleService: RoleService,
              private router: Router) { }
  model: Performance;
  id: string;
  times: { time:string, id:string }[] = [];
  count: number = 0;
  hasAccess: boolean = false;

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.id = params['id'];
      this.service.getItem(this.id)
        .subscribe(item => this.initModel(item));
    });
    this.hasAccess = this.roleService.hasAdminRole();
  }

  private initModel(item: Performance) {
    this.model = item;
    for (var i = 0; i < this.model.performanceDates.length; ++i) {
      var date = new Date(this.model.performanceDates[i].date.toString());
      for (var j = 0; j < this.model.performanceDates[i].performanceTimes.length; ++j) {
        var time = new Date(this.model.performanceDates[i].performanceTimes[j].time.toString());
        var month = (date.getMonth() < 9 ? '0' : '') + (date.getMonth() + 1);
        var day = (date.getDate() < 10 ? '0' : '') + date.getDate();
        var hours = (time.getHours() < 10 ? '0' : '') + time.getHours();
        var minutes = (time.getMinutes() < 10 ? '0' : '') + time.getMinutes();
        var tm = { time: '', id: '' };
        tm.time = '' + date.getFullYear() + '.' + month + '.' + day + ' ' + hours + ':' + minutes;
        tm.id = this.model.performanceDates[i].performanceTimes[j].id;
        this.times.push(tm);
      }
    };
  }

  book(id, timeId) {
    if (this.authService.isAuthenticated()) {
      this.service.bookPerformance(id, timeId, this.count)
        .subscribe((result) => {
          console.log(result)
        },
          error => console.log(error)
        ); // тут делается в случае положительного результата делается переадрессация на страницу заказа
    }
  }

  edit(id) {
    this.router.navigate(['/admin', id]);
  }
}
