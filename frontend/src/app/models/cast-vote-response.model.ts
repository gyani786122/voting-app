import { Candidate } from './candidate.model';
import { Voter } from './voter.model';

/**
 * Response from casting a vote operation.
 */
export interface CastVoteResponse {
  candidate: Candidate;
  voter: Voter;
}
