import { Component, Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
}

@Injectable()
export  class SortService {

    constructor() { }

    private columnSortedSource = new Subject<ColumnSortedEvent>();

    columnSorted$ = this.columnSortedSource.asObservable();

    columnSorted(event: ColumnSortedEvent) {
        this.columnSortedSource.next(event);
    }

}

export interface ColumnSortedEvent {
    sortColumn: string;
    sortDirection: string;
}