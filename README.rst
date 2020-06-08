MODify NETlist: A tool for processing verilog netlists and inserting fault injections at RTL level.
===================================================================================================
.. image:: https://circleci.com/gh/LCSR-lab/MODNET.svg?style=shield
   :target: https://circleci.com/gh/LCSR-lab/MODNET
.. image:: https://coveralls.io/repos/github/LCSR-lab/MODNET/badge.svg?branch=master
   :target: https://coveralls.io/github/LCSR-lab/MODNET?branch=master


MODNET is a tool that takes the Netlist of a circuit which it will submit, making changes to the places that are considered sensitive, so that the fault injection possible. The HDL code of the circuit to be submitted is synthesized using the "Synplify Pro" synthesis tool. In this tool you choose for which type of technology you want to synthesize the HDL code, it generates the Netlist in Verilog of the device under test (DUT). The generated Netlist is put in the entry in the MODNET tool (Modify NETlist), developed to automate the process of modification of the Xilinx libraries. Some of the modified components are the FD (flip-flop D) and its different copies (FDC, FDE ... etc) and also the LUTs (Look-Up Table), logic gates and multiplexers are included. The exit MODNET is the modified Netlist with a large number of extra input signals used to inject the flaws into the logs and logic gates, in this way It is possible to prepare an RTL so that SEUs and SETs can be injected. In this sense the original internal architecture is not changed and is respected as it is.

Main Features
-------------

* Simple implementation to create SEU and SET injections in Synplify Pro/Premier verilog netlists
* Easly extendible for other netlists sources (Vivado, Yosys, etc.)
* Thought from the begining to be automated into pipelines (fault-injection as a service)



Installing
----------
    
Install using pip:

.. code-block:: python

    pip install mod-net