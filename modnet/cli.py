#!/usr/bin/python3

import click

from .modnet import analysis


@click.command()
@click.option(
    '--verbose', '-v', 
    is_flag=True, 
    help="Will print verbose messages."
)

@click.option(
    '--netlist', '-n', 
    required=True, 
    help='Path where to find netlist'
)

@click.option(
    '--top-module', '-t', 
    required=True,
    help='Top module inside netlist'
)

@click.option(
    '--outdir', '-o', 
    default='./output', 
    help='Path whete to save resulting netlist with injections'
)

@click.option(
    '--mode', '-m', 
    default='synplify',  
    type=click.Choice(['synplify']),
    help='Mode defines the netlist origin synthesizer'
)

def main(verbose,netlist,top_module,mode,outdir):
    """
    MODify NETlist: A tool for processing verilog netlists and inserting fault injections at RTL level.
    """
    if verbose:
        click.echo("We are in the verbose mode.")

    a = analysis.Analysis(netlist,outdir,top_module)
    a.run()
    
if __name__ == "__main__":
    main()
