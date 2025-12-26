import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Candidate } from '../models/candidate.model';

@Injectable({
  providedIn: 'root'
})
export class CandidatesService {
  private apiUrl = 'http://localhost:5000/api/candidates';

  constructor(private http: HttpClient) {}

  /**
   * Retrieves all candidates from the API.
   */
  getAllCandidates(): Observable<Candidate[]> {
    return this.http.get<Candidate[]>(this.apiUrl);
  }

  /**
   * Creates a new candidate.
   * @param name The name of the candidate to create.
   */
  createCandidate(name: string): Observable<Candidate> {
    return this.http.post<Candidate>(this.apiUrl, { name });
  }
}
