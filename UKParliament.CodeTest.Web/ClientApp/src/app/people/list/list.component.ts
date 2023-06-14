import { Component, ViewChild, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { first } from 'rxjs/operators';
import { Person } from '../../models/person-view-model';
import { PersonService } from '../../_services/person.service';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Subject } from 'rxjs';


//initialise the columns for mat table
export const PRColumns = [
  {
    key: 'id',
    label: 'Id',
  },
  {
    key: 'title',
    label: 'Title',
  },
  {
    key: 'firstName',
    label: 'First Name',
  },
  {
    key: 'lastName',
    label: 'Last Name',
  },
  {
    key: 'dateOfBirth',
    type: 'date',
    label: 'Date Of Birth',
  },
  {
    key: 'gender',
    label: 'Gender',
  },
  {
    key: 'isEdit',
    type: 'isEdit',
    label: '',
  },
];



@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {


  displayedColumns: string[] = PRColumns.map((col) => col.key);
  dataSource = new MatTableDataSource<Person>([]);
  isLoading = false;
  columnsSchema: any = PRColumns;


  @Input() peopleList: any[] = [];
  @Output() delete = new EventEmitter<any>();
  @Output() edit = new EventEmitter<any>();
  people: any[] = [];
  @Input() changing: Subject<boolean> = new Subject<boolean>;

  constructor(private personService: PersonService) {

    //exclude id from the mat tbale display columns
    var fltr = this.displayedColumns.filter(obj => (obj !== 'id'));
    this.displayedColumns = fltr;
  }



  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  @ViewChild(MatPaginator) paginator: any = MatPaginator;

  ngOnInit() {

    //assign the datasource from the input
    this.dataSource.data = this.peopleList;
    this.dataSource.paginator = this.paginator;


    //notified when datasource array changes(add/edit) as array changes will not be captured on ngOnChanges
    //this.changing.subscribe((v: any) => {

    //  debugger;
    //  this.dataSource.data = this.peopleList;
    //  this.dataSource.paginator = this.paginator;
    //});
  }

  ngOnChanges(changes: SimpleChanges) {

    //re-assign the datasource from the input when it changes in manage-view component
    this.dataSource.data = this.peopleList; 
    this.dataSource.paginator = this.paginator;
  }

  deletePerson(id: number) {
    //emit to manage-view component
    this.delete.emit(id);
  }

  editPerson(id: number) {
    //emit to manage-view component
    this.edit.emit(id);
  }

}
