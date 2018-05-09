${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;    
    using Typewriter.Extensions.WebApi;

    // Uncomment the constructor to change template settings.
    Template(Settings settings)
    {
        settings.IncludeCurrentProject();
        settings.IncludeProject("HD.Resistor.Common");
        //settings.OutputFilenameFactory = (f) => 
        //{
        //    return "api.ts";
        //};
        settings.OutputExtension = ".tsx";
    }
}
${
    string Array(Class first) => "[]";
    string Concat(string first, string second) => first + second;
    string Format(string format, params object[] args) => string.Format(format, args);
    string CapFirst(string value) => value.Length == 0 ? string.Empty : value.Length == 1 ? value.ToUpper() : string.Format("{0}{1}", value.Substring(0, 1), value.Substring(1, value.Length - 1));
    string ClassNameCapFirst(Type first) => CapFirst(first.Name);
    // Custom extension methods can be used in the template by adding a $ prefix e.g. $LoudName
    string ReturnType(Method m) => m.Type.Name == "IHttpActionResult" ? "void" : m.Type.Name;

    Type ReturnBaseClass(Method m) => m.Type.Unwrap();
    string ServiceName(Class c) => c.Name.Replace("Controller", "Service");
    string MethodFormat(Method objMethod)
    {
        if(objMethod.HttpMethod() == "get"){
            return  $"<{objMethod.Type.Name}>(Url)";
        } 
        
        if(objMethod.HttpMethod() == "post"){
            return  $"(_Url, {objMethod.Parameters[0].name})";
        }
        if(objMethod.HttpMethod() == "put"){
            return  $"(_Url, {objMethod.Parameters[1].name})";
        }
        if(objMethod.HttpMethod() == "delete"){
            return  $"(_Url)";
        }
        
        return  $"";
    }

    // $Classes/Enums/Interfaces(filter)[template][separator]
    // filter (optional): Matches the name or full name of the current item. * = match any, wrap in [] to match attributes or prefix with : to match interfaces or base classes.
    // template: The template to repeat for each matched item
    // separator (optional): A separator template that is placed between all templates e.g. $Properties[public $name: $Type][, ]

    // More info: http://frhagn.github.io/Typewriter/

    /*
interface $Name_State {
    loading: boolean;
    data: $Name$Array;
}]
$ReturnBaseClass[interface $ClassName {
    $Properties(p => !p.Attributes.Any(a => a.Name == "JsonIgnore"))[
    $Name: $Type;]
}]
    */
}
$Enums([JsonExport])[
export enum $Name {
    $Values[
    $Name = $Value,]
}]

$Interfaces([JsonExport])[
$Methods(m => m.Type.Attributes.Any(a => a.Name == "JsonExport"))[
$Type[$Properties(p => p.Type.IsEnum)[$Type[
import { $Name } from './$Name';]]]
$Type[$Properties(p => p.Type.IsEnumerable)[$Type[
import { $ClassName } from './$ClassName';]]]
$Type[$Properties(p => !p.Type.IsEnumerable && !p.Type.IsEnum)[$Type[$Properties(p => p.Type.Attributes.Any(a => a.Name == "JsonExport"))[$Type[
import { $ClassName } from './$ClassName';
]]]]]]

$Properties(p => p.Type.IsEnum)[$Type[
import { $Name } from './$Name';]]
$Properties(p => p.Type.IsEnumerable)[$Type[
import { $ClassName } from './$ClassName';]]
$Properties(p => !p.Type.IsEnumerable && !p.Type.IsEnum)[$Type[$Properties(p => p.Type.Attributes.Any(a => a.Name == "JsonExport"))[$Type[
import { $ClassName } from './$ClassName';]]]]

export interface $Name {
    $Properties(p => !p.Attributes.Any(a => a.Name == "JsonIgnore"))[
    $Name: $Type,]
    $Methods(p => !p.Attributes.Any(a => a.Name == "JsonIgnore"))[
    $Name($Parameters[$name: $Type][, ]): $Type,]
}]

$Classes([JsonExport])[  
$Properties(p => p.Type.IsEnum)[$Type[
import { $Name } from './$Name';]]
$Properties(p => p.Type.IsEnumerable)[$Type[
import { $ClassName } from './$ClassName';]]
$Properties(p => !p.Type.IsEnumerable && !p.Type.IsEnum)[$Type[$Properties(p => p.Type.Attributes.Any(a => a.Name == "JsonExport"))[$Type[
import { $ClassName } from './$ClassName';]]]]

export interface $Name {
    $Properties(p => !p.Attributes.Any(a => a.Name == "JsonIgnore"))[
    $Name: $Type;]
}]

$Classes([ApiExport])[
import * as React from "react";
import { RouteComponentProps } from "react-router";
import "isomorphic-fetch";

$Methods[
$ReturnBaseClass[$Properties([JsonExport])[$Type[
import { $Name } from './$Name';
]]]
$ReturnBaseClass[$Properties(p => p.Type.IsEnum)[$Type[
import { $Name } from './$Name';
]]]
$ReturnBaseClass[$Properties(p => p.Type.IsEnumerable)[$Type[
import { $ClassName } from './$ClassName';
]]]
$ReturnBaseClass[$Properties(p => !p.Type.IsEnumerable && !p.Type.IsEnum)[$Type[$Properties(p => p.Type.Attributes.Any(a => a.Name == "JsonExport"))[$Type[
import { $ClassName } from './$ClassName';
]]]]]
interface $ReturnBaseClass[$ClassNameCapFirst]_State {
    loading: boolean;
    result: $ReturnType;
}
export class $Parent_$Name extends React.Component<RouteComponentProps<{}>, $ReturnBaseClass[$ClassNameCapFirst]_State> {
    constructor() {
        super();
        this.state = { result: $ReturnBaseClass[$Default], loading: true };

        fetch('$Url')
            .then(response => response.json() as Promise<$ReturnType>)
            .then(data => {
                this.setState({ result: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : $Parent_$Name.renderTable(this.state.result);

        return <div>
            <h1>Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            { contents }
        </div>;
    }

    private static renderTable(data: $ReturnType) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
            {data.map(x =>
                <tr key={ x.RGB }>
                    <td>{ x.ColorType }</td>
                    <td>{ x.Name }</td>
                    <td>{ x.Code }</td>
                    <td>{ x.Val }</td>
                </tr>
            )}
            </tbody>
        </table>;
    }
}]]

