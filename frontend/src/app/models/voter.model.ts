/**
 * Represents a voter in the voting system.
 */
export interface Voter {
  id: string;
  name: string;
  hasVoted: boolean;
  createdAtUtc: string;
}
