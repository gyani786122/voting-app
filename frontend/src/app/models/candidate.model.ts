/**
 * Represents a candidate in the voting system.
 */
export interface Candidate {
  id: string;
  name: string;
  voteCount: number;
  createdAtUtc: string;
}
