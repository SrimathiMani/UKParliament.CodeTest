import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';

import { PersonService } from '../../_services/person.service';
import { Person, Gender } from '../../models/person-view-model';
import { Subject } from 'rxjs';
import { AlertDialogComponent } from '../alert-dialog/alert-dialog.component';


@Component({
  selector: 'app-manage-view',
  templateUrl: './manage-view.component.html',
  styleUrls: ['./manage-view.component.scss']
})
export class ManageViewComponent implements OnInit {

  constructor(private personService: PersonService, private dialog: MatDialog) { }
  peopleList: Person[] = [];
  genderList: any[] = [];
  changingValue: Subject<boolean> = new Subject();

  showAddEditForm: boolean = false;

  //holds the id of the record that is being edited
  updateId: any = '';

  ngOnInit(): void {

    this.GetGenderEnumValues();
    this.GetAllPeople();
  }


  GetGenderEnumValues() {
    this.personService.getGenderValues()
      .subscribe(genderList => {
        this.genderList = genderList;
      });
  }


  GetAllPeople() {
    this.personService.getAll()
      .pipe(first())
      .subscribe(peopleLi => {
        this.peopleList = peopleLi;
        this.peopleList.forEach(x => x.gender == Gender.Male ? x.gender = "Male" : x.gender = "Female");
        this.changingValue.next(true);
      });
  }

  addPerson() {
    this.showAddEditForm = true;
    this.updateId = '';
  }

  savePeople(data: any) {
    if (data.id) {
      //Edit Person
      data.gender = data.gender == "1" ? Gender.Male : Gender.Female;
      return this.personService.update(data).subscribe(res => {
        let itemIndex = this.peopleList.findIndex(item => item.id == data.id);
        data.gender == Gender.Male ? data.gender = "Male" : data.gender = "Female";
        this.peopleList[itemIndex] = data;

        //ngOnChanges does not detect changes in array - so create a new object
        this.peopleList = [...this.peopleList];

        //close add/edit form
        this.showAddEditForm = false;
        this.updateId = '';
        this.changingValue.next(true);

        this.openAlertDialog("Person updated successfully!")
      });
    }
    else {
      //Create Person
      data.gender = data.gender == "1" ? Gender.Male : Gender.Female;
      return this.personService.create(data).subscribe(person => {
        person.gender == Gender.Male ? person.gender = "Male" : person.gender = "Female";
        this.peopleList.push(person);
        this.peopleList = [...this.peopleList];

        //close add/edit form
        this.showAddEditForm = false;
        this.updateId = '';
        this.changingValue.next(true);

        this.openAlertDialog("Person added successfully!")
      });
    }
  }

  deletePerson(id: any) {
    //const person = this.peopleList!.find(x => x.id === id);
    this.personService.delete(id)
      .pipe(first())
      .subscribe(() => {
        this.peopleList = this.peopleList!.filter(x => x.id !== id)

        //if update editor is open for this deleted row, then cancel the editor
        if (id == this.updateId) {
          this.showAddEditForm = false;
          this.updateId = '';
        }
      });
  }

  //cancel(hide) add/edit component
  closeAddEditForm(data: any) {
    this.showAddEditForm = false;
    this.updateId = '';
  }

  //show add/edit component and pass the id of the record being edited
  editPerson(id: any) {
    this.showAddEditForm = true;
    this.updateId = id;
  }

  openAlertDialog(mess: string) {
    const dialogRef = this.dialog.open(AlertDialogComponent, {
      data: {
        message: mess,
        buttonText: {
          cancel: 'Done'
        }
      },
    });
  }

}
