import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

//import { environment } from '../../environments/environment';
//const baseUrl = `${environment.apiUrl}/api/Person`;

import { Person } from '../models/person-view-model';
import { Observable } from 'rxjs';


  @Injectable({ providedIn: 'root' })
  export class PersonService {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrlNew: string) { }

    getAll(): Observable<Person[]> {
      return this.http.get<Person[]>(this.baseUrlNew + "api/person");
    }

    getById(id: number): Observable<Person> {
      return this.http.get<Person>(`${this.baseUrlNew + "api/person"}/${id}`);
    }

    Filter(name: string): Observable<Person[]> {
      return this.http.get<Person[]>(`${this.baseUrlNew + "api/person"}/SearchPeople?name=` + name);
    }

    create(person: Person): Observable<any> {
      return this.http.post(this.baseUrlNew + "api/person", person);
    }

    update(person: Person): Observable<any> {
      return this.http.put(this.baseUrlNew + "api/person", person);
    }

    delete(id: number) {
      return this.http.delete(`${this.baseUrlNew + "api/person"}/${id}`);
    }
  }

