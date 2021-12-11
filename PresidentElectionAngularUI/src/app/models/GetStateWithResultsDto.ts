import { GetFormattedResultDto } from "./GetFormattedResultDto";

export class GetStateWithResultsDto
    {
        Id: number;
        Name: string;
        Results: Array<GetFormattedResultDto>;
    }