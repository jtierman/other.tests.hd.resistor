





  

import { ResistorColorTypes } from './ResistorColorTypes';

import { Tolerance } from './Tolerance';


export interface ResistorColor {
    
    ColorType: ResistorColorTypes;
    Name: string;
    Code: string;
    RAL: number;
    Val: number;
    RGB: number;
    Tolerances: Tolerance[];
}



