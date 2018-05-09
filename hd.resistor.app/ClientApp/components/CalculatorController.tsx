








import * as React from "react";
import { RouteComponentProps } from "react-router";
import "isomorphic-fetch";
import { ResistorColor } from "./ResistorColor";






interface number_State {
    bands: string[];
    loading: boolean;
    selected: number;
    result: number;
}
export class CalculatorController_CalculateOhmValue extends React.Component<RouteComponentProps<{}>, number_State> {
    constructor() {
        super();
        this.state = { bands: ["", "", "", ""], result: 0, selected: 0, loading: true };
    }

    public go(a: string, b: string, c: string, d: string) {
        var args = [a, b, c, d];
        var url = 'api/Calculator?a=${0}}&b=${1}&c=${2}&d=${3}'.replace(/\$\{(\d)\}/, x => args[ +x ]);
        fetch(url)
            .then(response => response.json() as Promise<number>)
            .then(data => {
                this.setState({ result: data, loading: false });
            });
    }
    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <p><em></em> Ohms</p>

        return <div className='row'>
            <div className='col-sm-12'>
                {this.renderTable(this.state.bands)}
            </div>
            <div className='col-sm-9'>
                <h1>Result</h1> {this.state.result} <i>Ohms</i>
            </div>
            </div>;
    }

    private renderTable(data: string[]) {
        var header = data.slice(0);
        var columns = data.slice(0);
        var selected = this.state.selected;
        var ugly = (selected == 0) ? 'outset' : 'inset';
        return <table>
            <thead>
                {header.map((x, i) =>
                    <th style={{ textOverflow: 'none', textAlign: 'center' }}>{ String.fromCharCode('A'.charCodeAt(0)+i) }</th>
                )}
            </thead>
            <tbody>
                <tr>
                    {columns.map((x, j) =>
                        <td style={{ borderStyle: { ugly }, border: '1px 1px 1px 1px' }}>
                            <button key={j} onClick={(e) => { this.setState({ selected: j }); e.stopPropagation(); }} style={{ width: '50px', height: '50px' }} />
                        </td>
                    )}
                </tr>
            </tbody>
        </table>;
    }
}

