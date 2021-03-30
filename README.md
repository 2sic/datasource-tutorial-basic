# EAV DataSources - Tutorial - Basic for 2sxc 11.13+

Demo / tutorial code to get started with your own custom data source.

## Full Documentation

This below is just an extract. You can find the full infos / docs here:

https://docs.2sxc.org/net-code/data-sources/custom/tutorial-basic/index.html

## What's inside

1. A Visual Studio 2019 solution for 2sxc/EAV 11.13+ 
1. Three DataSources
1. Test code to test the DataSources


## DataSource `Basic`

The Basic DataSource is very trivial - it just return a single Entity with todays Date and some information like the Day-Name.

## DataSource `BasicList`

This delivers a list of random dates.

## DataSource `ConfigurableDateTime`

This is a fairly advanced example, containing

1. Configuration
  1. Configuration of a DataSource with Tokens and Settings
  1. A Configuration Content-Type stored in the `.data/contenttypes` folder
  1. A class `RegisterGlobalContentTypes` which registers that folder for global Content-Type
1. Error Reporting
  1. A property which should always contain `Today` or an error will be streamed
  1. A number property which will stream an error if it's not a number or > 23
  1. General error catching

