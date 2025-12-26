import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Voter } from '../models/voter.model';

@Injectable({
  providedIn: 'root'
})
export class VotersService {
  private apiUrl = 'http://localhost:5000/api/voters';

  constructor(private http: HttpClient) {}

  /**
   * Retrieves all voters from the API.
   */
  getAllVoters(): Observable<Voter[]> {
    return this.http.get<Voter[]>(this.apiUrl);
  }

  /**
   * Creates a new voter.
   * @param name The name of the voter to create.
   */
  createVoter(name: string): Observable<Voter> {
    return this.http.post<Voter>(this.apiUrl, { name });
  }
}
