import { Component, OnInit, Input, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Person } from '../../models/person-view-model';

import { PersonService } from '../../_services/person.service';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.scss']
})
export class AddEditComponent implements OnInit {
  form!: FormGroup;
  title!: string;
  loading = false;
  submitting = false;
  submitted = false;
  hideDiv = true;
  maxdate: any = new Date().toLocaleDateString();

  @Input() id?: number = 0;
  @Output() save = new EventEmitter<any>();
  @Output() cancel = new EventEmitter<any>();

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private personService: PersonService
  ) { }

  ngOnChanges(changes: SimpleChanges) {
    this.LoadData();
  }

  ngOnInit() {

    this.InitialiseForm();
    this.LoadData();
  }

  InitialiseForm() {
    this.form = this.formBuilder.group({
      id: [],
      title: ['', [Validators.required]],
      firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20), Validators.pattern('[a-zA-Z]+')]],
      lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20), Validators.pattern('[a-zA-Z]+')]],
      dateOfBirth: ['', [Validators.required]
      ],
      gender: ['', [Validators.required]]
    });
  }
  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  LoadData() {
    this.title = 'Add Person';
    if (this.id) {
      // edit mode
      this.title = 'Edit Person';
      this.loading = true;

      //call service to get the details of the edit record
      this.personService.getById(this.id)
        .pipe(first())
        .subscribe(x => {
          this.form.patchValue(x);
          this.loading = false;
        });
    }
    else {
      //reset the form
      this.InitialiseForm();
      this.loading = false;
    }
  }

  Cancel() {
    //emit to manage-view component
    this.cancel.emit();
  }

  onSubmit() {

    this.submitted = true;

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.submitting = true;

    //emit to manage-view component
    this.save.emit(this.form.value);
  }

  getToday(): string {
    return new Date().toISOString().split('T')[0]
  }
}
