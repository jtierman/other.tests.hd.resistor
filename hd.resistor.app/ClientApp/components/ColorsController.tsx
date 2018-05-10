








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
export class ColorsController_Get extends React.Component<{ setColor(color: string): any; }, ResistorColor_State> {
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
        const me = this.state;

        let contents = me.loading
            ? <p><em>Loading...</em></p>
            : this.renderTable(me.result);

        let typeChoices = [ResistorColorTypes.None, ResistorColorTypes.Digit, ResistorColorTypes.Multiplier];

        return <div>
            <select onChange={(e) => { this.go(typeChoices[e.target.selectedIndex]); e.stopPropagation(); }}>
                {typeChoices.map((v, i) =>
                    <option value={i}>{ResistorColorTypes[v]}</option>
                )}
            </select>
            { contents }
        </div>;
    }

    private renderTable(data: ResistorColor[]) {
        return <table className='table'>
            <thead>
                <th>Name</th>
                <th>Code</th>
            </thead>
            <tbody>
                {data.map(x =>
                    <tr key={x.Name}
                        style={{ backgroundColor: x.Name, cursor: 'pointer' }}
                        onClick={(e) => { this.props.setColor(x.Name); e.stopPropagation(); }}>
                        <td>{x.Name}</td>
                        <td>{x.Code}</td>
                </tr>
                )}
            </tbody>
            </table>;
    }
}

