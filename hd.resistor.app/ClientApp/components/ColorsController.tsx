








import * as React from "react";
import { RouteComponentProps } from "react-router";
import "isomorphic-fetch";




import { ResistorColorTypes } from './ResistorColorTypes';


import { Tolerance } from './Tolerance';
import { ResistorColor } from "./ResistorColor";


interface ResistorColor_State {
    loading: boolean;
    type: ResistorColorTypes;
    result: ResistorColor[];
}
export class ColorsController_Get extends React.Component<RouteComponentProps<{}>, ResistorColor_State> {
    constructor() {
        super();
        this.state = { result: [], type: ResistorColorTypes.None, loading: true };
    }

    public go(types: ResistorColorTypes)
    {
        var url = 'api/Colors/'.concat(types.toString());
        fetch(url)
            .then(response => response.json() as Promise<ResistorColor[]>)
            .then(data => {
                this.setState({ result: data, loading: false });
            });
    }
    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : ColorsController_Get.renderTable(this.state.result);

        let typeChoices = [ResistorColorTypes.None, ResistorColorTypes.Digit, ResistorColorTypes.Multiplier];

        return <div>
            <h1>ColorType: {this.state.type.toString()}</h1>
            <select onChange={(e) => this.go(typeChoices[e.target.selectedIndex])}>
                {typeChoices.map((v, i) =>
                    <option value={i}>{ResistorColorTypes[v]}</option>
                )}
            </select>
            { contents }
        </div>;
    }

    private static renderTable(data: ResistorColor[]) {
        return <table className='table'>
            <thead>
                <th>Name</th>
                <th>Code</th>
            </thead>
            <tbody>
                {data.map(x =>
                    <tr key={x.RGB} onClick={(e) => { }}>
                    <td>{x.Name}</td>
                    <td>{x.Code}</td>
                </tr>
                )}
            </tbody>
            </table>;
    }
}

