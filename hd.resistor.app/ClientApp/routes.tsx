import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
//import { ColorsController_Get } from './components/ColorsController';
import { CalculatorController_CalculateOhmValue } from "./components/CalculatorController";

   // <Route path='/' component={ColorsController_Get} />
export const routes = <Layout>
    <Route path='/' component={CalculatorController_CalculateOhmValue} />
</Layout>;
