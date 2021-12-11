import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';

@Injectable({
  providedIn: 'root'
})
export class ResultsService {

  private apiGetResults = 'https://localhost:44340/api/States/formatted';
  private apiFileUpload = 'https://localhost:44340/api/File';

  constructor(private http: HttpClient) { }

  getAllResults() {
    return this.http.get(this.apiGetResults);
  }


  public uploadResultsFile(file: File): Observable<object> {
    // create multipart form for file
    let formData: FormData = new FormData();
    formData.append('file', file, file.name);

    const headers = new HttpHeaders().append('Content-Disposition', 'mulipart/form-data');

    return this.http
      .post(this.apiFileUpload, formData, { headers: headers })
      .pipe(map(response => response));
  }
}
