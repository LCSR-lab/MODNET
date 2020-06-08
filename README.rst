MODify NETlist: A tool for processing verilog netlists and inserting fault injections at RTL level.
===================================================================================================
.. image:: https://circleci.com/gh/LCSR-lab/MODNET.svg?style=shield
   :target: https://circleci.com/gh/LCSR-lab/MODNET
.. image:: https://coveralls.io/repos/github/LCSR-lab/MODNET/badge.svg?branch=master
   :target: https://coveralls.io/github/LCSR-lab/MODNET?branch=master

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