import { Injectable, } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

import { catchError } from 'rxjs/operators';
import { Concert } from 'src/app/shared/models/concert.model';
import { ConcertService } from 'src/app/shared/services/concert.service';

@Injectable()
export class ConcertResolver implements Resolve<Concert> {
  constructor(
    private concertService: ConcertService,
    private router: Router
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {

    return this.concertService.getById(route.params['id'])
      .pipe(catchError((err) => this.router.navigateByUrl('/')));
  }
}