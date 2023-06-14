import {Component, Inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {PersonViewModel} from "../../models/person-view-model";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  // Below is some sample code to help get you started calling the API

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getPersonById(1);
  }

  getPersonById(id: number): void {
    this.http.get<PersonViewModel[]>(this.baseUrl + `api/person/${id}`).subscribe(result => {
    }, error => console.error(error));
  }
}
